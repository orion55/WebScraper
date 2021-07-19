using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using HtmlAgilityPack;
using System.IO;

namespace WebScraper
{
    public class Document
    {
        private readonly string _url;
        private readonly ILogger _log;
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

            int count = nodes.Count;
            if (count > 0)
            {
                this._items = new string[count];

                for (int i = 0; i < count; i++)
                    this._items[i] = nodes[i].InnerText;

                Console.WriteLine(String.Join(", ", this._items));
            }
            else
            {
                throw new Exception("The parsing result is empty");
            }
        }

        public void WriteFile()
        {
            string dirPath = Directory.GetCurrentDirectory() + "\\out";
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists) dirInfo.Create();

            string filePath = dirPath + "\\out.txt";
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists) fileInfo.Delete();

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, false, System.Text.Encoding.Default))
                {
                    if (this._items != null && this._items.Length > 0)
                    {
                        for (int i = 0; i < this._items.Length; i++)
                            streamWriter.WriteLine(this._items[i]);
                        
                        this._log.Information("The result is written to a file " + filePath);
                    }
                    else
                    {
                        this._log.Error("The array parsing is empty");
                    }
                }
            }
            catch (Exception ex)
            {
                this._log.Error(ex, ex.Message);
            }
        }
    }
}