using prktikod2;
using System.Text.RegularExpressions;
static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
var html = await Load("https://hebrewbooks.org/beis");

var cleanHtml = new Regex("\\s+").Replace(html, " ");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml)
                                    .Select(s => s.Trim())
                                    .Where(s => s.Length > 0)
                                    .ToList();
HtmlElement root = new HtmlElement { Name = "root" };
HtmlElement currentElement = root;
foreach (var line in htmlLines)
{
    string firstWord = line.Split(' ')[0];
    if (firstWord == "/html")
    {
        break;
    }
    if (firstWord.StartsWith("/"))
    {
        if (currentElement.Parent != null)
            currentElement = currentElement.Parent;
    }
    else {
        string tagName = firstWord.TrimEnd('/');
        if (HtmlHelper.Instance.arr1.Contains(firstWord))
        {
            HtmlElement htmlElement=new HtmlElement { Name = firstWord, Parent = currentElement };
            currentElement.Children.Add(htmlElement);
            string restOfLine = line.Substring(firstWord.Length).Trim();
            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(restOfLine);

            foreach (Match attr in attributes)
            {
                string attrName = attr.Groups[1].Value;   
                string attrValue = attr.Groups[2].Value; 

                htmlElement.Attrubutes.Add(attr.Value);   

                if (attrName == "id")
                    htmlElement.Id = attrValue;

                if (attrName == "class")
                    htmlElement.Classes.AddRange(attrValue.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            }
            if (!(HtmlHelper.Instance.arr2.Contains(firstWord) || line.EndsWith('/')))
            {
                currentElement = htmlElement;
            }

           
        }
        else
        currentElement.InnerHtml += line + " ";

    }




   
}


Selector selector = Selector.FromQueryString("div");

var results = root.FindElements(selector);

Console.WriteLine($"נמצאו {results.Count} אלמנטים");

foreach (var element in results)
{
    Console.WriteLine($"{element.Name} | id: {element.Id} | classes: {string.Join(" ", element.Classes)}");
}

Console.ReadLine();


