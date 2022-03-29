using Fatca.Core;
using Fatca.Core.Configurations;
using Fatca.Core.Services;
using Fatca.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fatca.Test
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            Console.WriteLine(new TimeSpan(13, 0, 0).Duration());

            Console.WriteLine(new DateTime(2020, 12, 11).ToFormatedString());

            RegisterServices(args);
            //var compressionService = new CompressionService(); //Publisher
            //var subscriber = new CompressionSubscriber();

            //compressionService.FileCompressed += subscriber.OnFileCompressed;

            //compressionService.Compress(new CompressibleFile() { FileName = "FATCA Fiile" });



            var report = FatcaFactory.CreateFatca();
            string message = !string.IsNullOrEmpty(report?.Version) ? "Operation Successsful" : "Operation Failed";
            Console.WriteLine(message);

            // Excel Reader
            List<FatcaReport> reports = FatcaReportManager.ReadFromExcel(@"C:\Users\Surface\Documents\Clients\Access\FATCA\App\Fatca\Fatca.Test\FatcaReportTemplate.xlsx");
            if (reports is { } && reports.Any())
                reports.ForEach(report => { Console.WriteLine(report.ToString()); });
            //Generate FATCA Files
            var fatca = FatcaFactory.CreateFatca(reports, new DateTime(2020, 12, 31));

            var setting = serviceProvider.GetService<ReportSettings>();

            //Process FATCA files

            string path = setting.FilePath;// @"C:\Users\Surface\Documents\Clients\Access\FATCA\App\Fatca\Fatca.Test\TestFiles";
            var client = serviceProvider.GetService<FatcaClient>();
            var fileProcessingService = new ProcessingService(); //Publisher
            fileProcessingService.FileProcessed += OnFileCompressed;
            client.processingService = fileProcessingService;
            var reportSettings = serviceProvider.GetService<ReportSettings>();
            try
            {
                // FatcaClient.Create().Execute(path, true);
                client.Execute(path, true);

                if (client.HasError)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    client.Errors.ToList().ForEach(c =>
                    {
                        Console.WriteLine($"{c.Key}=>{c.Value}");
                    });
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Disponse();

            Console.ReadKey();
        }

        static void RegisterServices(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            // services.confi
            services.AddTransient(typeof(XmlFactory));

            services.AddTransient(typeof(FatcaFactory));
            services.AddSingleton<ReportSettings>();
            services.AddTransient(typeof(FatcaProcessor));
            services.AddTransient(typeof(FatcaClient));
            var settings = Configuration.GetOptions<FatcaSettings>("FatcaSetting");
            services.AddSingleton(settings);
            var reportSettings = Configuration.GetOptions<ReportSettings>("ReportSettings");
            services.AddSingleton(reportSettings);

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging();

            // services.AddSingleton<FatcaSettings>();
            serviceProvider = services.BuildServiceProvider();

        }

        static void Disponse()
        {
            if (serviceProvider is null)
            {
                return;
            }
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }

        }

        public static void OnFileCompressed(object source, ProcessingEventArgs args)
        {
            Console.WriteLine("Works from console"); ;
        }
    }


    public static class ConfigurationExtensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
    public static class DateExtension
    {
        public static string ToFormatedDate(this DateTime date)
          => string.Format("{0:dd-MMM-yy}", date);

        public static string ToFormatedString(this DateTime date)
         => string.Format("{0:dd-MMM-yyyy}", date);

    }
}