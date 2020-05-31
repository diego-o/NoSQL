using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoSQL.Infrastructure.Tools
{
    public static class JsonTools
    {
        public static string ReadJson(string path)
        {
            using (var reader = new StreamReader(path))
                return reader.ReadToEnd();
        }
    }
}
