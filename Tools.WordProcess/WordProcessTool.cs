using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
