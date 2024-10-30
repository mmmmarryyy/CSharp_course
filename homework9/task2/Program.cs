public class Foo 
{
    public void first() { print("first"); }
    public void second() { print("second"); }
    public void third() { print("third"); }

    private void print(string text)
    {
        Console.Write(text);
    }
}

class Program
{
    private static ManualResetEvent firstDone = new ManualResetEvent(false);
    private static ManualResetEvent secondDone = new ManualResetEvent(false);

    static void Main(string[] args)
    {
        Foo foo = new Foo();

        Thread threadA = new Thread(() => {
            foo.first();
            firstDone.Set();
        });

        Thread threadB = new Thread(() => {
            firstDone.WaitOne();
            foo.second();
            secondDone.Set();
        });

        Thread threadC = new Thread(() => {
            secondDone.WaitOne();
            foo.third();
        });

        Thread[] threads = { threadA, threadB, threadC };

        Random rng = new Random();

        var shuffledThreads = threads.OrderBy(_ => rng.Next()).ToList();

        Console.WriteLine("[DEBUG] Shuffled ordering:");
        for (int i = 0; i < shuffledThreads.Count; i++)
        {
            if (shuffledThreads[i] == threadA)
            {
                Console.WriteLine($"[DEBUG] Thread {i + 1}: first");
            }
            else if (shuffledThreads[i] == threadB)
            {
                Console.WriteLine($"[DEBUG] Thread {i + 1}: second");
            }
            else if (shuffledThreads[i] == threadC)
            {
                Console.WriteLine($"[DEBUG] Thread {i + 1}: third");
            }
        }
        Console.WriteLine();

        for (int i = 0; i < shuffledThreads.Count; i++)
        {
            shuffledThreads[i].Start();
            Thread.Sleep(2000);
        }

        for (int i = 0; i < shuffledThreads.Count; i++)
        {
            shuffledThreads[i].Join();
        }
    }
}

/*
Output can look like:

```
[DEBUG] Shuffled ordering:
[DEBUG] Thread 1: third
[DEBUG] Thread 2: first
[DEBUG] Thread 3: second

firstsecondthird
```
or
```
[DEBUG] Shuffled ordering:
[DEBUG] Thread 1: second
[DEBUG] Thread 2: third
[DEBUG] Thread 3: first

firstsecondthird
```
and so on
*/
