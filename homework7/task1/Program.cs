using System.Diagnostics;

public class Element
{
    public required string Name { get; init; }
}

public class Program
{
    public static string ConcatenateNames(List<Element> elements, string delimiter)
    {
        if (elements.Count < 3)
        {
            return "";
        }

        return string.Join(delimiter, elements.Skip(3).Select(element => element.Name).ToList());
    }

    public static void Main(string[] args)
    {
        List<Element> elements = new List<Element>
        {
            new Element { Name = "Element 1" },
            new Element { Name = "Element 2" },
            new Element { Name = "Element 3" },
            new Element { Name = "Element 4" },
            new Element { Name = "Element 5" },
        };

        string concatenatedNames = ConcatenateNames(elements, ", ");
        Debug.Assert(concatenatedNames == "Element 4, Element 5");

        Console.WriteLine("Success");
    }
}
