using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Backend_Homework.Application.Configuration;
using Microsoft.Extensions.Hosting;
using Backend_Homework.DataAccess.Options;
using Backend_Homework.Application.Services;
using Backend_Homework.DataAccess.Services;
using Backend_Homework.ConsoleApp.DI;

namespace Continero.Homework
{
    public class Program
    {
        // https://stackoverflow.com/questions/68392429/how-to-run-net-core-console-app-using-generic-host-builder
        public async static Task Main(string[] args)
        {
            var currentEnvironment = EnvironmentHelper.GetCurrentEnvironment();
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile(Path.Combine(AssemblyDirectory, "appSettings.json"), false, true);
                    //configuration.AddJsonFile(Path.Combine(AssemblyDirectory, $"appSettings.{currentEnvironment:G}.json"), true, true);
                })
                .ConfigureServices((builder,services) =>
                    services
                        .AddLogging(logger => logger.AddConsole())
                        .Configure<FileSystemStorageOptions>(builder.Configuration.GetSection(FileSystemStorageOptions.FileSystemStorage))
                        .AddTransient<ControlService>()
                        .AddTransient<IDocumentService, DocumentService>()
                        .AddTransient<IStorageService, StorageService>()
                        .AddTransient<IConverterService, ConverterService>()
                        .AddStorages()
                        .AddConverters())
                .ConfigureLogging(configuration =>
                {
                    configuration.AddConsole();
                })                
                .Build();

            var logger = host.Services.GetService<ILoggerFactory>().CreateLogger<Program>();            
            logger.LogInformation("Starting application");

            var mainService = host.Services.GetRequiredService<ControlService>();
            await mainService.ExecuteAsync();                 

            logger.LogInformation("All done!");
        }

        

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().Location;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}