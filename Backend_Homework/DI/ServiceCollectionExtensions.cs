using Backend_Homework.Application.Converters.Json;
using Backend_Homework.Application.Converters.Xml;
using Backend_Homework.DataAccess.Storages.Cloud;
using Backend_Homework.DataAccess.Storages.FileSystem;
using Backend_Homework.DataAccess.Storages.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Backend_Homework.ConsoleApp.DI
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddStorages(this IServiceCollection services)
        {
            return services.AddTransient<IFileSystemStorage, FileSystemStorage>()
                        .AddTransient<ICloudStorage, AzureStorage>()
                        .AddTransient<IWebStorage, WebStorage>();
        }
        internal static IServiceCollection AddConverters(this IServiceCollection services)
        {
            return services.AddTransient<IJsonConverter, JsonConverter>()
                        .AddTransient<IXmlConverter, XmlConverter>();
        }
        
    }
}
