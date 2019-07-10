using DuplicateImageDetector.Service;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace DuplicateImageDetector
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            var servicesProvider = BuildDi(config);

            Console.WriteLine("PLease provide the File Directory path");

            var DirectoryPath = Console.ReadLine();
            using (servicesProvider as IDisposable)
            {
                var runner = servicesProvider.GetRequiredService<ImageDuplicateDetectorService>();
                var result = runner.ImageDuplicates(DirectoryPath);

                Console.WriteLine("Press ANY key to exit");
                Console.ReadKey();
            }

        }

        //Add Service Here
        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddTransient<ImageDuplicateDetectorService>() 
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }
    }
}
