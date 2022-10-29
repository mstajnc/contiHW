using Backend_Homework.DataAccess;
using Backend_Homework.DataAccess.Services;
using Backend_Homework.DataAccess.Storage;
using Backend_Homework.DataAccess.Storages.Cloud;
using Backend_Homework.DataAccess.Storages.FileSystem;
using Backend_Homework.DataAccess.Storages.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ILogger<DocumentService> _logger;
        private readonly IStorageService _storageService;
        private readonly IConverterService _converterService;

        public DocumentService(ILogger<DocumentService> logger,
            IStorageService storageService,
            IConverterService converterService)
        {
            _logger = logger;
            _storageService = storageService;
            _converterService = converterService;
        }

        public async Task<bool> CheckIfFileExists(StorageType storageType, string fileName)
        {
            _logger.LogDebug($"Document service checks if {fileName} in {storageType} exists");
            return await _storageService.CheckIfFileExists(storageType, fileName);
            
        }

        public Task<string> FormatText(string text, FormatType resultFormatType)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetDataFromFile<T>(StorageType storageType, string fileName)
        {
            _logger.LogDebug($"Document service retrieves {fileName} from {storageType}");
            var fileContent = await _storageService.GetTextFromFile(storageType, fileName);
            var fileExtension = Path.GetExtension(fileName);

            return await _converterService.ConvertStringToObject<T>(fileContent, fileExtension);
        }
    }
}
