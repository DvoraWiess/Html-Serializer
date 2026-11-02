using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace prktikod2
{
    internal class HtmlHelper
    {
        public string[] arr1 { get; set; }
        public string[] arr2 { get; set; }
        private static HtmlHelper _instance=new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        private HtmlHelper()
        {
            var content1 = File.ReadAllText("JSON Files/HtmlTags.json");
            var content2 = File.ReadAllText("JSON Files/HtmlVoidTags.json");
            arr1= JsonSerializer.Deserialize<string[]>(content1);
            arr2= JsonSerializer.Deserialize<string[]>(content2);
        }
    }
}
