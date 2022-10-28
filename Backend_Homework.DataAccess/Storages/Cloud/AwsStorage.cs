namespace Backend_Homework.DataAccess.Storages.Cloud
{
    public class AwsStorage : ICloudStorage
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
