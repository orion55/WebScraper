using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using HtmlAgilityPack;

namespace WebScraper
{
    public class Document
    {
        private readonly string _url;
        private ILogger _log;
        private string[] _items;

        public Document(IConfigurationRoot settings, ILogger log)
        {
            string url = settings.GetValue<string>("url");

            if (String.IsNullOrEmpty(url))
                throw new Exception("The \"url\" value is empty");

            this._url = url;
            this._log = log;

            this._log.Information("Parse url: {0}", this._url);
        }

        public void Parser()
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(this._url);
            var nodes = htmlDoc.DocumentNode
                .SelectNodes("//div[@class='article__text']/div/h2/a");

            this._items = new string[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
                this._items[i] = nodes[i].InnerText;
            
            
            Console.WriteLine(String.Join(", ", this._items));
        }
    }
}