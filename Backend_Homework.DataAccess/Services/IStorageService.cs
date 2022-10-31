namespace Backend_Homework.DataAccess.Services
{
    public interface IStorageService
    {
        Task<bool> CheckIfFileExists(StorageType storageType, string fileName);
        Task<string> GetTextFromFile(StorageType storageType, string fileName);
        Task SaveDocument(StorageType storageType, string text, string fileExtension);
    }
}
