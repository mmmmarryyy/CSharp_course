using System.Diagnostics;

class Program
{
    public static string StringyFib(int n)
    {
        if (n < 2)
        {
            return "invalid";
        }

        List<string> fibStrings = new List<string>();
        fibStrings.Add("b");
        fibStrings.Add("a");

        for (int i = 2; i < n; i++)
        {
            fibStrings.Add(fibStrings[i - 1] + fibStrings[i - 2]);
        }

        return string.Join(", ", fibStrings);
    }

    static void Main(string[] args)
    {
        Debug.Assert(StringyFib(1) == "invalid");
        Debug.Assert(StringyFib(2) == "b, a");
        Debug.Assert(StringyFib(3) == "b, a, ab");
        Debug.Assert(StringyFib(7) == "b, a, ab, aba, abaab, abaababa, abaababaabaab");

        Console.WriteLine("Success");
    }
}
