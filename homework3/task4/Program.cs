using System.Diagnostics;

class Program
{
    static int GCD(int a, int b)
    {
        if (a == 0)
        {
            return b;
        }
        if (b == 0)
        {
            return a;
        }

        if (a > b)
        {
            return GCD(a % b, b);
        }
        else
        {
            return GCD(a, b % a);
        }
    }

    static string Simplify(string arg)
    {
        string[] numbers = arg.Split('/');
        if (numbers.Count() == 2)
        {
            int secondNumber = int.Parse(numbers[1]);

            if (secondNumber == 0)
            {
                throw new ArgumentException("Division by zero");
            }

            bool isPositive = true;

            if (secondNumber < 0)
            {
                isPositive = !isPositive;
                secondNumber = -secondNumber;
            }

            int firstNumber = int.Parse(numbers[0]);
            if (firstNumber < 0)
            {
                isPositive = !isPositive;
                firstNumber = -firstNumber;
            }

            int gcd = GCD(firstNumber, secondNumber);
            if (gcd == secondNumber) 
            {
                return string.Format("{0}{1}", isPositive ? "" : "-", firstNumber / gcd);
            } 
            else
            {
                return string.Format("{0}{1}/{2}", isPositive ? "" : "-", firstNumber / gcd, secondNumber / gcd);
            }
        }
        else
        {
            throw new ArgumentException("Wrong arg format");
        }
    }

    static void Main(string[] args)
    {
        Debug.Assert(Simplify("4/6") == "2/3");
        Debug.Assert(Simplify("10/11") == "10/11");
        Debug.Assert(Simplify("100/400") == "1/4");
        Debug.Assert(Simplify("8/4") == "2");
        Debug.Assert(Simplify("-4/6") == "-2/3");
        Debug.Assert(Simplify("4/-6") == "-2/3");
        
        try 
        {
            Simplify("4/0");
            Debug.Fail("no exception thrown");
        }
        catch (Exception ex)
        {
            Debug.Assert(ex is ArgumentException);
        }
        
        Console.WriteLine("Success");
    }
}
