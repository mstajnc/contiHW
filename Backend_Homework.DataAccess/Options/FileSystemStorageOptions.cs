namespace Backend_Homework.DataAccess.Options
{
    public class FileSystemStorageOptions
    {
        public const string FileSystemStorage = "FileSystemStorage";

        public string RootPath { get; set; }
        public string SourceFolderName { get; set; }
        public string TargetFolderName { get; set; }
    }
}