using System;
using Microsoft.Extensions.Configuration;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Settings();
            Console.WriteLine(settings.Configuration.GetValue<string>("url"));
        }
    }
}