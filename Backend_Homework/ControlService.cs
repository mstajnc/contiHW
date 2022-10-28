using Backend_Homework.Application.Services;
using Backend_Homework.ConsoleApp;
using Backend_Homework.DataAccess;
using Backend_Homework.DataAccess.Options;
using Backend_Homework.DataAccess.Storages.Cloud;
using Backend_Homework.DataAccess.Storages.FileSystem;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Continero.Homework
{
    internal class ControlService
    {
        private readonly ILogger<ControlService> _logger;
        private readonly IOptions<FileSystemStorageOptions> _settings;
        private readonly DocumentService _documentService;

        public ControlService(ILogger<ControlService> logger, IOptions<FileSystemStorageOptions> settings, DocumentService documentService)
        {
            _logger = logger;
            _settings = settings;
            _documentService = documentService;
        }

        internal async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            _logger.LogInformation("Doing something");
            _logger.LogInformation(_settings.Value.RootPath);
            
            var fileExistsInStorage = false;

            while (!fileExistsInStorage)
            {
                var fileName = GetFileNameDialog();
                fileExistsInStorage = await ChooseStorageDialog(fileName);
            }
            
            
            

        }

        private string GetFileNameDialog()
        {
            Console.WriteLine($"Enter the name of the file");
            var input = Console.ReadLine(); //TODO: validate user input
            _logger.LogDebug($"{nameof(GetFileNameDialog)} - User's input:{input}");
            return input;
        }

        private async Task<bool> ChooseStorageDialog(string fileName)
        {
            Console.WriteLine($"Select storage type:{Environment.NewLine}{UserActions.GetFormattedOutput(UserActions.StorageOptions)}");
            var input = Console.ReadKey().Key;
            _logger.LogDebug($"{nameof(ChooseStorageDialog)} - User's input:{input}");

            var storageType = input switch
            {
                ConsoleKey.F => StorageType.FileSystem,
                ConsoleKey.A => StorageType.Cloud,
                ConsoleKey.W => StorageType.Web,
                _ => throw new NotImplementedException()
            };
                
            var fileExists = await _documentService.CheckIfFileExists(storageType, fileName);
            if (!fileExists)
            {
                Console.WriteLine($"File {fileName} does not exist in {storageType}, please try again");
            }

            return fileExists;            
        }


    }
}