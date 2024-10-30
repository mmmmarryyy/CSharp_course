class Program
{
    static void Main(string[] args)
    {
        int sharedCounter = 0;

        for (int i = 0; i < 500; i++)
        {
            sharedCounter = 0;

            Thread thread1 = new Thread(() => {
                Thread.Sleep(50);
                sharedCounter = sharedCounter + 2;
            });

            Thread thread2 = new Thread(() => {
                Thread.Sleep(50);
                sharedCounter = sharedCounter + 2;
            });

            Thread thread3 = new Thread(() => {
                Thread.Sleep(50);
                sharedCounter = sharedCounter + 2;
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            if (sharedCounter != 6)
            {
                Console.WriteLine($"[i = {i}] Data race detected! Counter = {sharedCounter}, but should be equal to 6");
            }
        }

        Console.WriteLine("End.");
    }
}

/*
Output will look something like:

[i = 128] Data race detected! Counter = 4, but should be equal to 6
[i = 174] Data race detected! Counter = 2, but should be equal to 6
[i = 307] Data race detected! Counter = 4, but should be equal to 6
[i = 330] Data race detected! Counter = 4, but should be equal to 6
[i = 339] Data race detected! Counter = 4, but should be equal to 6
[i = 358] Data race detected! Counter = 2, but should be equal to 6
[i = 367] Data race detected! Counter = 4, but should be equal to 6
[i = 381] Data race detected! Counter = 4, but should be equal to 6
[i = 411] Data race detected! Counter = 2, but should be equal to 6
[i = 463] Data race detected! Counter = 4, but should be equal to 6
[i = 476] Data race detected! Counter = 4, but should be equal to 6
[i = 494] Data race detected! Counter = 4, but should be equal to 6
End.
*/