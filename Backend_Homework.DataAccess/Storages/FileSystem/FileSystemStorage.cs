using Backend_Homework.DataAccess.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.DataAccess.Storages.FileSystem
{
    public class FileSystemStorage : IFileSystemStorage
    {
        public Task<bool> DocumentExists(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadDocument(string key)
        {
            throw new NotImplementedException();
        }

        public Task SaveDocument(string text, string documentExtension)
        {
            throw new NotImplementedException();
        }
    }
}
