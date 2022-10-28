namespace Backend_Homework.DataAccess.Storages.Web
{
    public class WebStorage : IWebStorage
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
