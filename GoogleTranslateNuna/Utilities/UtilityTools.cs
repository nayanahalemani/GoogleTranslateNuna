using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTranslateNuna.Utilities
{
    public class UtilityTools
    {
        public static string GetFilePath(string fileName)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string path = projectDirectory.Replace("\\bin", "");
            string directoryPath = Path.Combine(String.Join(@"\", path.Split('\\')), fileName);
            return directoryPath;
        }
        public static JObject GetJsonData(string fileName)
        {
            JObject jsonObj = JObject.Parse(File.ReadAllText(GetFilePath(fileName)));
            return jsonObj;
        }

    }
}
