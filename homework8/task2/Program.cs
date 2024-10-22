public class Program
{
    private static SemaphoreSlim _semaphoreT1 = new SemaphoreSlim(1);
    private static SemaphoreSlim _semaphoreT2 = new SemaphoreSlim(0);

    private static void T1()
    {
        for (int i = 1; i <= 10; i++)
        {
            _semaphoreT1.Wait();
            Console.WriteLine($"1: Строка {i}");
            _semaphoreT2.Release();
        }
    }

    private static void T2()
    {
        for (int i = 1; i <= 10; i++)
        {
            _semaphoreT2.Wait();
            Console.WriteLine($"2: Строка {i}");
            _semaphoreT1.Release();
        }
    }

    public static void Main(string[] args)
    {
        Thread threadT1 = new Thread(T1);
        Thread threadT2 = new Thread(T2);

        threadT1.Start();
        threadT2.Start();

        threadT1.Join();
        threadT2.Join();
    }
}