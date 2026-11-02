using prktikod2;

internal static class HtmlElementExtensions
{
    public static HashSet<HtmlElement> FindElements(this HtmlElement element, Selector selector)
    {
        HashSet<HtmlElement> result = new HashSet<HtmlElement>();
        Recorse(element, selector, result);
        return result;
    }

    private static void Recorse(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
    {
        var matching = element.Descendants()
                              .Where(e => e != element)
                              .Where(e => IsMatch(e, selector));

        if (selector.Child == null)
        {
            foreach (var e in matching)
                result.Add(e);              
        }
        else
        {
            foreach (var e in matching)
                Recorse(e, selector.Child, result);   
        }
    }

    private static bool IsMatch(HtmlElement e, Selector selector)
    {
        if (selector.TagName != null && e.Name != selector.TagName)
            return false;
        if (selector.Id != null && e.Id != selector.Id)
            return false;
        if (!selector.Classes.All(c => e.Classes.Contains(c)))
            return false;
        return true;
    }
}