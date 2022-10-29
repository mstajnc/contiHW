namespace Backend_Homework.Application.Services
{
    public interface IConverterService
    {
        Task<T> ConvertStringToObject<T>(string value, string fileExtension);
        Task<string> ConvertObjectToString(object obj, FormatType formatType);
        string[] GetFormatTypeFileExtensions(FormatType formatType);
    }
}
