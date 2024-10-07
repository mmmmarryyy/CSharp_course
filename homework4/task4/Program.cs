using System.Diagnostics;

class Program
{
    static int SunLoungers(string beach)
    {
        int count = 0;
        int previous = -2; 

        for (int i = 0; i < beach.Length; i++)
        {
            if (beach[i] == '0' && ((i < beach.Length-1 && beach[i+1] == '0') || (i == beach.Length-1)) && (previous != i-1)) {
                count++;
                previous = i;
            }
        }

        return count;
    }

    static void Main(string[] args)
    {
        Debug.Assert(SunLoungers("10001") == 1);
        Debug.Assert(SunLoungers("00101") == 1);
        Debug.Assert(SunLoungers("0") == 1);
        Debug.Assert(SunLoungers("000") == 2);

        Console.WriteLine("Success");
    }
}
