
using AspNetCore.Serilog.RequestLoggingMiddleware;
using Serilog.Events;

namespace Serilog.WebAPI.Testing.Application
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Logging.SetSerilog();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();


            app.MapControllers();

            app.Run();
        }

        static void SetSerilog(this ILoggingBuilder logger)
        {
            // https://stackoverflow.com/questions/61544047/use-serilog-with-microsoft-extensions-logging-ilogger
            // https://medium.com/@brucycenteio/adding-serilog-to-asp-net-core-net-7-8-5cba1d0dea2
            // https://stackoverflow.com/questions/46018230/c-sharp-where-does-console-writeline-came-from
            // https://github.com/mthamil/AspNetCore.Serilog.RequestLoggingMiddleware/blob/master/README.md
            // https://stackoverflow.com/questions/71599246/how-to-configure-and-use-serilog-in-asp-net-core-6

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Async(a => a.File(
                    path:"./Abhishek.testing.log",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Warning))
                .CreateLogger();

            //Log.Logger = new LoggerConfiguration().WriteTo.File(
            //        path: "c:\\hotellisting\\log\\log-.txt",
            //        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
            //        rollingInterval: RollingInterval.Day,
            //        restrictedToMinimumLevel: LogEventLevel.Warning
            //    ).CreateLogger();

            logger.AddConsole();
            logger.AddSerilog(Log.Logger, dispose: true);
            logger.AddDebug();
            logger.SetMinimumLevel(LogLevel.Trace);
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
