using System;
using Serilog;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Settings();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Start scraping...");

            try
            {
                Document doc = new Document(settings.Configuration, Log.Logger);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}