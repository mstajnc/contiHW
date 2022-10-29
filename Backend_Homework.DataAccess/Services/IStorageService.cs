using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.DataAccess.Services
{
    public interface IStorageService
    {
        Task<bool> CheckIfFileExists(StorageType storageType, string fileName);

        Task<string> GetTextFromFile(StorageType storageType, string fileName);
    }
}
