class Program
{
    private static Mutex mutexA = new Mutex();
    private static Mutex mutexB = new Mutex();

    static void Thread1Method()
    {
        mutexA.WaitOne();
        Console.WriteLine("Thread 1 acquired mutex A");
        Thread.Sleep(1000);

        Console.WriteLine("Thread 1 before acquiring mutex B");
        mutexB.WaitOne();
        Console.WriteLine("Thread 1 acquired mutex B");

        mutexB.ReleaseMutex();
        mutexA.ReleaseMutex();
    }

    static void Thread2Method()
    {
        mutexB.WaitOne();
        Console.WriteLine("Thread 2 acquired mutex B");
        Thread.Sleep(1000);

        Console.WriteLine("Thread 2 before acquiring mutex A");
        mutexA.WaitOne();
        Console.WriteLine("Thread 2 acquired mutex A");

        mutexA.ReleaseMutex();
        mutexB.ReleaseMutex();
    }

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(Thread1Method);
        Thread thread2 = new Thread(Thread2Method);

        thread1.Start();
        thread2.Start();

        Console.WriteLine("Threads started.");
        
        thread1.Join();
        thread2.Join();

        Console.WriteLine("After threads join.");
    }
}
