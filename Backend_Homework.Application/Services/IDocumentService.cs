using Backend_Homework.DataAccess;

namespace Backend_Homework.Application.Services
{
    public interface IDocumentService
    {
        Task<bool> CheckIfFileExists(StorageType storageType, string fileName);
        Task<T> GetDataFromFile<T>(StorageType storageType, string fileName);
        Task<string> Convert(object obj, FormatType resultFormatType);
        Task SaveFile(StorageType storage, string fileContent, FormatType resultFormatType);
    }
}
