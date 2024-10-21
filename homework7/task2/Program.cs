using System.Diagnostics;

public class Program
{
    public static List<string> filterList(IEnumerable<string> list)
    {
        return list.Where((element, index) => element.Length > index).ToList();
    }

    public static void Main(string[] args)
    {
        List<string> names = new List<string> { "John", "Jane", "Alice", "Bob", "Charlie" };
        List<string> expectedList = new List<string> { "John", "Jane", "Alice", "Charlie" };
        List<string> filteredNames = filterList(names);

        Debug.Assert(filteredNames.Count == 4);
        Debug.Assert(filteredNames.SequenceEqual(expectedList));

        Console.WriteLine("Success");
    }
}
