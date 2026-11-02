using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prktikod2
{
    internal class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attrubutes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement()
        {
            Classes = new List<string>();
            Attrubutes = new List<string>();
            Children = new List<HtmlElement>();
        }
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0) 
            {
                HtmlElement current = queue.Dequeue();
                yield return current;
                foreach (HtmlElement child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement curren = this.Parent;
            while (curren != null)
            {
                yield return curren;
                curren = curren.Parent;
            }
        }
    }
}
