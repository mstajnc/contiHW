using System.Text.Json;

namespace Backend_Homework.Application.Converters.Json
{
    public class JsonConverter : IJsonConverter
    {
        private static readonly string[] _fileExtensions = new[] { "json" };
        public string[] FileExtensions => _fileExtensions;

        public async Task<string> Convert(object source)
        {
            return JsonSerializer.Serialize(source);
        }

        public async Task<T> ParseText<T>(string text)
        {
            return JsonSerializer.Deserialize<T>(text);
        }
    }
}
