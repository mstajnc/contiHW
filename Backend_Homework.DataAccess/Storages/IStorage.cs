using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.DataAccess.Storage
{
    public interface IStorage
    {
        Task<string> ReadDocument(string key);

        Task SaveDocument(string text, string documentExtension);
    }
}
