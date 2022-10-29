using Backend_Homework.DataAccess.Storage;
using Backend_Homework.DataAccess.Storages.Cloud;
using Backend_Homework.DataAccess.Storages.FileSystem;
using Backend_Homework.DataAccess.Storages.Web;

namespace Backend_Homework.DataAccess.Services
{
    public class StorageService : IStorageService
    {
        private readonly IFileSystemStorage _fileSystemStorage;
        private readonly ICloudStorage _cloudStorage;
        private readonly IWebStorage _webStorage;
        private readonly Dictionary<StorageType, IStorage> _storageTypeStorages;

        public StorageService(IFileSystemStorage fileSystemStorage,
            ICloudStorage cloudStorage,
            IWebStorage webStorage)
        {
            _fileSystemStorage = fileSystemStorage;
            _cloudStorage = cloudStorage;
            _webStorage = webStorage;
            _storageTypeStorages = new Dictionary<StorageType, IStorage>()
            {
                [StorageType.FileSystem] = _fileSystemStorage,
                [StorageType.Cloud] = _cloudStorage,
                [StorageType.Web] = _webStorage
            };
        }

        public async Task<bool> CheckIfFileExists(StorageType storageType, string fileName)
        {
            return await _storageTypeStorages[storageType].DocumentExists(fileName);
        }

        public async Task<string> GetTextFromFile(StorageType storageType, string fileName)
        {
            return await _storageTypeStorages[storageType].ReadDocument(fileName);
        }
    }
}
