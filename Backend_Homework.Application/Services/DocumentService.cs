using Backend_Homework.DataAccess;
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
    public class DocumentService
    {
        private readonly ILogger<DocumentService> _logger;
        private readonly IFileSystemStorage _fileSystemStorage;
        private readonly ICloudStorage _cloudStorage;
        private readonly IWebStorage _webStorage;

        public DocumentService(ILogger<DocumentService> logger,
            IFileSystemStorage fileSystemStorage,
            ICloudStorage cloudStorage,
            IWebStorage webStorage)
        {
            _logger = logger;
            _fileSystemStorage = fileSystemStorage;
            _cloudStorage = cloudStorage;
            _webStorage = webStorage;
        }

        public async Task<bool> CheckIfFileExists(StorageType storageType, string fileName)
        {
            _logger.LogDebug($"Document service checks if {fileName} in {storageType} exists");
            return storageType switch
            {
                StorageType.Cloud => await _cloudStorage.DocumentExists(fileName),
                StorageType.FileSystem => await _fileSystemStorage.DocumentExists(fileName),
                StorageType.Web => await _webStorage.DocumentExists(fileName),
                _ => throw new NotImplementedException()
            };
        }
    }
}
