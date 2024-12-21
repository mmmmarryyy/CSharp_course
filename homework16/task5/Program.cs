using System.Text;
using System.Diagnostics;

class Program
{
    public static string RationalNumber(long a, long b)
    {
        if (a == 0) return "0";
        if (b == 0) throw new DivideByZeroException();
        if (a >= b) throw new ArgumentException("a must be less than b");


        StringBuilder result = new StringBuilder();
        result.Append("0.");

        Dictionary<long, int> remainderIndex = new Dictionary<long, int>();
        long remainder = a;
        int index = 0;

        while (remainder != 0 && !remainderIndex.ContainsKey(remainder))
        {
            remainderIndex[remainder] = index++;
            remainder *= 10;
            result.Append(remainder / b);
            remainder %= b;
        }

        if (remainder == 0)
        {
            return result.ToString();
        }
        else
        {
            int periodStart = remainderIndex[remainder];
            return result.Insert(periodStart + 2, "(").Append(")").ToString();
        }
    }


    static void Main(string[] args)
    {
        Debug.Assert(RationalNumber(2, 5) == "0.4");
        Debug.Assert(RationalNumber(1, 6) == "0.1(6)");
        Debug.Assert(RationalNumber(1, 3) == "0.(3)");
        Debug.Assert(RationalNumber(1, 7) == "0.(142857)");
        Debug.Assert(RationalNumber(1, 77) == "0.(012987)");
        Debug.Assert(RationalNumber(1, 999) == "0.(001)");

        Console.WriteLine("Success");
    }
}
