using Backend_Homework.Application.Converters;
using Backend_Homework.Application.Converters.Json;
using Backend_Homework.Application.Converters.Xml;

namespace Backend_Homework.Application.Services
{
    public class ConverterService : IConverterService
    {
        private readonly Dictionary<FormatType, IConverter> _formatTypeConverters;
        private readonly IJsonConverter _jsonConverter;
        private readonly IXmlConverter _xmlConverter;

        public ConverterService(IJsonConverter jsonConverter, IXmlConverter xmlConverter)
        {
            _jsonConverter = jsonConverter;
            _xmlConverter = xmlConverter;
            _formatTypeConverters = new()
            {
                [FormatType.Json] = _jsonConverter,
                [FormatType.Xml] = _xmlConverter,
            };
        }
        public async Task<T> ConvertStringToObject<T>(string text, string fileExtension)
        {
            return await _formatTypeConverters.SingleOrDefault(x => x.Value.FileExtensions.Contains(fileExtension)).Value.ParseText<T>(text);
        }

        public async Task<string> ConvertObjectToString(object obj, FormatType formatType)
        {
            throw new NotImplementedException();
        }

       
    }
}
