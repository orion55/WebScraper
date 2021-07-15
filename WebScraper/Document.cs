using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace WebScraper
{
    public class Document
    {
        private string _url;
        private ILogger _log;


        public Document(IConfigurationRoot settings, ILogger log)
        {
            string url = settings.GetValue<string>("url");

            if (String.IsNullOrEmpty(url))
                throw new Exception("The \"url\" value is empty");

            this._url = url;
            this._log = log;
            
            this._log.Information("Url: {0}", this._url);
        }
    }
}