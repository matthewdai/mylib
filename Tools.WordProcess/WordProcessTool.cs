using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace Tools.WordProcess
{
    public static class WordProcessTool
    {
        public static string RemoveEmptyLine(string text)
        {
            // split text by line
            var lines = text.Split('\n');

            var result = new StringBuilder();

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    result.Append(line.Trim());
                    result.Append("\r\n");
                }
            }
            return result.ToString();
        }


        public static string GetTextFromUrl(string url)
        {
            //WebRequest request = WebRequest.Create(url);
            //WebResponse response = request.GetResponse();
            //Stream dataStream = response.GetResponseStream();
            //StringReader reader = new StringReader(dataStream);
            //string reasposeFromServer = reader.ReadToEnd();

            //var wb = new System.Windows.Controls.WebBrowser();
            //var page = wb.Document as HtmlDocument;
            //page.

            using(WebClient client = new WebClient())
            {
                var s = client.DownloadString(url);
                return s;
            }
        }


    }


   

}
