namespace Backend_Homework.Application.Converters
{
    public interface IConverter
    {
        string[] FileExtensions { get; }
        Task<T> ParseText<T>(string text);
        Task<string> Convert(object source);
    }
}
