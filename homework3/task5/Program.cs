using System.Diagnostics;

public class Program
{
    public static Int64 LuckyTicket(Int64 n)
    {
        if (n <= 0 || n % 2 != 0)
        {
            return 0;
        }

        Int64 upperBound = (Int64)Math.Pow(10, n / 2);
        var sums = new Dictionary<Int64, Int64>();

        for (Int64 i = 0; i < upperBound; i++)
        {
            Int64 sum = SumOfDigits(i);

            if (sums.ContainsKey(sum))
            {
                sums[sum]++;
            }
            else
            {
                sums.Add(sum, 1);
            }
        }

        return sums.Values.Select(count => count * count).Sum();
    }

    private static Int64 SumOfDigits(Int64 num)
    {
        Int64 sum = 0;
        while (num > 0)
        {
            sum += num % 10;
            num /= 10;
        }
        return sum;
    }

    public static void Main(string[] args)
    {
        Debug.Assert(LuckyTicket(2) == 10);
        Debug.Assert(LuckyTicket(4) == 670);
        Debug.Assert(LuckyTicket(12) == 39581170420);
        Console.WriteLine("Success");
    }
}