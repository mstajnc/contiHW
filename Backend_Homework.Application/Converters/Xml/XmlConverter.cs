using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.Application.Converters.Xml
{
    public class XmlConverter : IXmlConverter
    {
        private static readonly string[] _fileExtensions = new[] { "xml" };
        public string[] FileExtensions => _fileExtensions;

        public Task<string> Convert<T>(T source)
        {
            throw new NotImplementedException();
        }

        public Task<T> ParseText<T>(string text)
        {
            throw new NotImplementedException();
        }
    }
}
