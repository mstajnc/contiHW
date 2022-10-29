using Backend_Homework.Application;
using Backend_Homework.Application.BusinessObjects;
using Backend_Homework.Application.Services;
using Backend_Homework.ConsoleApp;
using Backend_Homework.DataAccess;
using Backend_Homework.DataAccess.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Continero.Homework
{
    internal class ControlService
    {
        private readonly ILogger<ControlService> _logger;
        private readonly IOptions<FileSystemStorageOptions> _settings;
        private readonly IDocumentService _documentService;

        public ControlService(ILogger<ControlService> logger, IOptions<FileSystemStorageOptions> settings, IDocumentService documentService)
        {
            _logger = logger;
            _settings = settings;
            _documentService = documentService;
        }

        internal async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            var (fileName, storage) = await SelectFileStep();
            await ConversionStep(fileName, storage);
        }

        private async Task ConversionStep(string fileName, StorageType storage)
        {
            _logger.LogDebug("Running {step}", nameof(ConversionStep));
            var format = ChooseFormatDialog();
            var fileContent = await _documentService.GetDataFromFile<Document>(storage, fileName);
            var formattedText = await _documentService.Convert(fileContent, format);

            await _documentService.SaveFile(storage, formattedText, format);
        }

        private async Task<(string, StorageType)> SelectFileStep()
        {
            _logger.LogDebug("Running {step}", nameof(SelectFileStep));
            var fileExistsInStorage = false;
            var fileName = string.Empty;
            var storage = StorageType.FileSystem;
            while (!fileExistsInStorage)
            {
                fileName = GetFileNameDialog();
                (fileExistsInStorage, storage)  = await ChooseStorageDialog(fileName);
            }
            return (fileName, storage);
        }

        private FormatType ChooseFormatDialog()
        {
            _logger.LogDebug("Running {dialog}", nameof(ChooseFormatDialog));

            var selectionOptions = new Dictionary<ConsoleKey, FormatType>()
            {
                [ConsoleKey.X] = FormatType.Xml,
                [ConsoleKey.J] = FormatType.Json,
                [ConsoleKey.B] = FormatType.Bson,
                [ConsoleKey.Y] = FormatType.Yaml,
            };

            var isFormatSelected = false;
            var selectedFormat = FormatType.Xml;

            while (!isFormatSelected)
            {
                Console.WriteLine($"Select format of the resulting file:{Environment.NewLine}{UserActions.GetFormattedOutput(UserActions.FormatOptions)}");
                var input = Console.ReadKey().Key;
                _logger.LogDebug("{dialogName} - User's input:{input}", nameof(ChooseFormatDialog), input);
                isFormatSelected = selectionOptions.TryGetValue(input, out selectedFormat);
            }

            return selectedFormat;
        }

        private string GetFileNameDialog()
        {
            _logger.LogDebug("Running {dialog}", nameof(GetFileNameDialog));

            Console.WriteLine($"Enter the name of the file (including file extension)");
            var input = Console.ReadLine(); //TODO: validate user input
            _logger.LogDebug("{dialogName} - User's input:{input}", nameof(GetFileNameDialog), input);
            return input ?? string.Empty;
        }

        private async Task<(bool, StorageType)> ChooseStorageDialog(string fileName)
        {
            _logger.LogDebug("Running {dialog}", nameof(ChooseStorageDialog));

            var selectionOptions = new Dictionary<ConsoleKey, StorageType>()
            {
                [ConsoleKey.F] = StorageType.FileSystem,
                [ConsoleKey.C] = StorageType.Cloud,
                [ConsoleKey.H] = StorageType.Web,
            };
            var isStorageSelected = false;
            var selectedStorage = StorageType.FileSystem;

            while (!isStorageSelected)
            {
                Console.WriteLine($"Select storage type:{Environment.NewLine}{UserActions.GetFormattedOutput(UserActions.StorageOptions)}");
                var input = Console.ReadKey().Key;
                _logger.LogDebug("{dialogName} - User's input:{input}", nameof(ChooseStorageDialog), input);
                isStorageSelected = selectionOptions.TryGetValue(input, out selectedStorage);
            }

            var fileExists = await _documentService.CheckIfFileExists(selectedStorage, fileName);
            if (!fileExists)
            {
                Console.WriteLine($"File {fileName} does not exist in {selectedStorage}, please try again");
            }

            return (fileExists, selectedStorage);
        }


    }
}