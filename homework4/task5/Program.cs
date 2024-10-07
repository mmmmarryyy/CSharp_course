using System.Diagnostics;

class Program
{
    static string ExpressFactors(int number) 
    {
        string result = "";

        for (int i = 2; i <= number; i++)
        {
            int count = 0;
            while (number % i == 0)
            {
                count++;
                number /= i;
            }

            if (count > 1) 
            {
                result += $" x {i}^{count}";
            } else if (count == 1)
            {
                result += $" x {i}";
            }
        }

        return result.Substring(3);
    }

    static void Main(string[] args)
    {
        Debug.Assert(ExpressFactors(2) == "2");
        Debug.Assert(ExpressFactors(4) == "2^2");
        Debug.Assert(ExpressFactors(10) == "2 x 5");
        Debug.Assert(ExpressFactors(60) == "2^2 x 3 x 5");
        Debug.Assert(ExpressFactors(3465) == "3^2 x 5 x 7 x 11");
        Debug.Assert(ExpressFactors(7680) == "2^9 x 3 x 5");
        Debug.Assert(ExpressFactors(9999) == "3^2 x 11 x 101");

        Console.WriteLine("Success");
    }
}
