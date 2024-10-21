using System.Diagnostics;
using static BookFormatter;

public class Program
{
    public static void Main(string[] args)
    {
        var dictionary = new Dictionary<string, string>()
        {
            {"this", "эта"},
            {"dog", "собака"},
            {"eats", "ест"},
            {"too", "слишком"},
            {"much", "много"},
            {"vegetables", "овощей"},
            {"after", "после"},
            {"lunch", "обеда"}
        };

        var formatter = new BookFormatter(dictionary, 3);

        Debug.Assert(
            formatter.FormatBook("This dog eats too much vegetables after lunch") == 
            "ЭТА СОБАКА ЕСТ\nСЛИШКОМ МНОГО ОВОЩЕЙ\nПОСЛЕ ОБЕДА"
        );

        Console.WriteLine("Success");
    }
}
