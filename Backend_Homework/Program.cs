using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Backend_Homework.Application.Configuration;
using System.Collections;
using Microsoft.Extensions.Hosting;

namespace Continero.Homework
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var currentEnvironment = EnvironmentHelper.GetCurrentEnvironment();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile(Path.Combine(AssemblyDirectory, "appSettings.json"), false, true);
                    configuration.AddJsonFile(Path.Combine(AssemblyDirectory, $"appSettings.{currentEnvironment:G}.json"), true, true);
                })
                .ConfigureServices(services =>
                    services.AddOptions()
                        .AddLogging(logger => logger.AddConsole())
                        .AddTransient<ControlService>())
                .ConfigureLogging(configuration =>
                {
                    configuration.AddConsole();
                })
                .Build();

            var logger = host.Services.GetService<ILoggerFactory>()
                .CreateLogger<Program>();            
            logger.LogWarning("Starting application");

            var mainService = host.Services.GetRequiredService<ControlService>();
            await mainService.ExecuteAsync();                 

            logger.LogDebug("All done!");

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