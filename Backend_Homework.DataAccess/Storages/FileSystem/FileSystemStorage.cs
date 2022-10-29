using Backend_Homework.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace Backend_Homework.DataAccess.Storages.FileSystem
{
    public class FileSystemStorage : IFileSystemStorage
    {
        private readonly string _targetPath;
        private readonly string _sourcePath;
        public FileSystemStorage(IOptions<FileSystemStorageOptions> options)
        {
            if (options.Value.RootPath is null)
            {
                throw new ArgumentNullException(nameof(options.Value.RootPath));
            }
            _targetPath = Path.Combine(options.Value.RootPath, options.Value.TargetFolderName);
            _sourcePath = Path.Combine(options.Value.RootPath, options.Value.SourceFolderName);
        }

        public async Task<bool> DocumentExists(string key)
        {
            if(_sourcePath == null) return false;
            return File.Exists(Path.Combine(_sourcePath, key));
        }

        public async Task<string> ReadDocument(string key)
        {
            return await File.ReadAllTextAsync(Path.Combine(_sourcePath, key));
        }

        public async Task SaveDocument(string text, string documentExtension)
        {
            //return await File.WriteAllTextAsync(Path.Combine(_targetPath, key), text);
        }
    }
}
