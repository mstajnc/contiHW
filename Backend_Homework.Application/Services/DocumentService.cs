using Backend_Homework.DataAccess;
using Backend_Homework.DataAccess.Services;
using Microsoft.Extensions.Logging;

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
            _logger.LogDebug("{serviceName} checks if {fileName} in {storageType} exists", nameof(DocumentService), fileName, storageType);
            return await _storageService.CheckIfFileExists(storageType, fileName);            
        }

        public async Task<string> Convert(object obj, FormatType resultFormatType)
        {
            _logger.LogDebug("{serviceName} converts object to {resultFormatType}", nameof(DocumentService), resultFormatType);

            var newContent = await _converterService.ConvertObjectToString(obj, resultFormatType);
            return newContent;
        }
        public async Task SaveFile(StorageType storage, string fileContent, FormatType resultFormatType)
        {
            _logger.LogDebug("{serviceName} save object to {storage} in format {resultFormatType}", nameof(DocumentService), storage, resultFormatType);

            var fileExtension = _converterService.GetFormatTypeFileExtensions(resultFormatType).First();
            await _storageService.SaveDocument(storage, fileContent, fileExtension);
        }

        public async Task<T> GetDataFromFile<T>(StorageType storageType, string fileName)
        {
            _logger.LogDebug("{serviceName} retrieves {fileName} from {storage}", nameof(DocumentService), fileName, storageType);

            var fileContent = await _storageService.GetTextFromFile(storageType, fileName);
            var fileExtension = Path.GetExtension(fileName);
            return await _converterService.ConvertStringToObject<T>(fileContent, fileExtension);
        }
    }
}
