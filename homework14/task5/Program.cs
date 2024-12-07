using System.Diagnostics;

class Program
{
    public static string Sorting(string str)
    {
        var letters = new List<char>();
        var digits = new List<char>();

        foreach (char c in str)
        {
            if (char.IsLetter(c))
            {
                letters.Add(c);
            }
            else if (char.IsDigit(c))
            {
                digits.Add(c);
            }
        }

        letters.Sort((x, y) =>
        {
            if (char.ToLower(x) != char.ToLower(y))
            {
                return char.ToLower(x).CompareTo(char.ToLower(y));
            }
            else
            {
                return char.IsLower(x) ? -1 : 1;
            }
        });

        digits.Sort();

        return string.Concat(letters) + string.Concat(digits);
    }

    static void Main(string[] args)
    {
        Debug.Assert(Sorting("eA2a1E") == "aAeE12");
        Debug.Assert(Sorting("Re4r") == "erR4");
        Debug.Assert(Sorting("6jnM31Q") == "jMnQ136");
        Debug.Assert(Sorting("846ZIbo") == "bIoZ468");

        Console.WriteLine("Success");
    }
}
