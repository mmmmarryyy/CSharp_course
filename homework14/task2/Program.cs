using System.Runtime.ExceptionServices;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            int result1 = Divide(10, 0);
            Console.WriteLine($"Division result: {result1}");
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo edi = ExceptionDispatchInfo.Capture(ex);

            Console.WriteLine("The exception is caught but not yet rethrown.");
            Console.WriteLine($"Exception type: {ex.GetType().Name}");
            Console.WriteLine($"Exception message: {ex.Message}");

            Console.WriteLine("\nRethrowing an exception...");
            try
            {
                RethrowException(edi);
            }
            catch (Exception rethrownEx)
            {
                Console.WriteLine($"Rethrown exception: {rethrownEx.GetType().Name}, Message: {rethrownEx.Message}");
            }
        }

        Console.WriteLine("The program is complete.");
    }

    static int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Division by zero!");
        }
        return a / b;
    }

    static void RethrowException(ExceptionDispatchInfo edi)
    {
        edi.Throw();
    }
}
