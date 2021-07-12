using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebScraper
{
    public class Settings
    {
        public Settings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
    }
}