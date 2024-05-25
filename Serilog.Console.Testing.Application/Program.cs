using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System;

namespace Serilog.Console.Testing.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello, World!");
            var iLogger = GetILogger("ConsoleProgram", "./test.log");

            // uses MS ILogger to write to console and file
            iLogger.LogInformation("this is a test");

            Log.CloseAndFlush();
            System.Console.ReadLine();
        }

        private static Microsoft.Extensions.Logging.ILogger GetILogger(string categoryName, string pathname)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Async(a => a.File(pathname))
                .CreateLogger();

            ILoggerFactory factory = new LoggerFactory().AddSerilog(Log.Logger);
            return factory.CreateLogger(categoryName);
        }

        private static ILoggerFactory GetILoggerFActory(string pathname = null)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Async(a => a.File(pathname))
                .CreateLogger();

            ILoggerFactory factory = new LoggerFactory().AddSerilog(Log.Logger);
            return factory;
        }

        private static Microsoft.Extensions.Logging.ILogger GetILogger<T>(string pathname = null)
        {
            // https://stackoverflow.com/questions/61544047/use-serilog-with-microsoft-extensions-logging-ilogger
            // https://medium.com/@brucycenteio/adding-serilog-to-asp-net-core-net-7-8-5cba1d0dea2
            // https://stackoverflow.com/questions/46018230/c-sharp-where-does-console-writeline-came-from
            // https://github.com/mthamil/AspNetCore.Serilog.RequestLoggingMiddleware/blob/master/README.md
            // https://stackoverflow.com/questions/71599246/how-to-configure-and-use-serilog-in-asp-net-core-6

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.Async(a => a.File(pathname))
               .CreateLogger();

            ILoggerFactory factory = new LoggerFactory().AddSerilog(Log.Logger);
            return factory.CreateLogger<T>();
        }
    }
}
