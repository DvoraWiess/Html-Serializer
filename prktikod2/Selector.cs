using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prktikod2
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector     Child { get; set; }
        public Selector Parent { get; set; }
        public Selector() {
            Classes = new List<string>();

        }
        private static Selector divider(string level) {
            Selector current = new Selector();
            string[] parts = Regex.Split(level, "(?=[#.])");

            foreach (string part in parts)
            {
                if (part.Length == 0)
                    continue;

                if (part.StartsWith("#"))
                    current.Id = part.Substring(1);
                else if (part.StartsWith("."))
                    current.Classes.Add(part.Substring(1));
                else if (HtmlHelper.Instance.arr1.Contains(part))
                    current.TagName = part;
            }
            return current;
        }
        public static Selector FromQueryString(string query)
        {
            Selector root = null;
            Selector prev = null;
            string[] levels = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < levels.Length; i++) { 
                string level = levels[i];
                Selector selector=divider(level);
                if (i == 0) 
                { root = selector; }
                else { 
                    selector.Parent = prev;
                    prev.Child = selector;
                }
                prev = selector;
            }
            return root;
        }

    }
}
