using System.Xml.Serialization;

namespace Backend_Homework.Application.Converters.Xml
{
    public class XmlConverter : IXmlConverter
    {
        private static readonly string[] _fileExtensions = new[] { ".xml" };
        public string[] FileExtensions => _fileExtensions;

        public async Task<string> Convert(object source)
        {
            using var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(source.GetType());
            serializer.Serialize(stringWriter, source);
            return stringWriter.ToString();
        }

        public async Task<T> ParseText<T>(string text)
        {
            using var stringReader = new StringReader(text);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }
    }
}
