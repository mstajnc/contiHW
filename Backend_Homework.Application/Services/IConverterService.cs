using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.Application.Services
{
    public interface IConverterService
    {
        Task<T> ConvertStringToObject<T>(string value, string fileExtension);
        Task<string> ConvertObjectToString(object obj, FormatType formatType);
    }
}
