public class FizzBuzz 
{ 
    private int n;
    private Barrier barrier = new Barrier(4);

    public FizzBuzz(int n) 
    {
        this.n = n;
    }

    // printFizz() outputs "fizz". 
    public void Fizz(Action printFizz) {
        for (int i = 1; i <= n; ++i)
        {
            if (i % 3 == 0 && i % 5 != 0)
            {
                printFizz();
            } 

            barrier.SignalAndWait();
        }
    }
    
    // printBuzz() outputs "buzz". 
    public void Buzz(Action printBuzz) {
        for (int i = 1; i <= n; ++i)
        {
            if (i % 5 == 0 && i % 3 != 0)
            {
                printBuzz();
            } 

            barrier.SignalAndWait();
        }
    }

    // printFizzBuzz() outputs "fizzbuzz". 
    public void Fizzbuzz(Action printFizzBuzz) {
        for (int i = 1; i <= n; ++i)
        {
            if (i % 15 == 0)
            {
                printFizzBuzz();
            } 

            barrier.SignalAndWait();
        }
    }

    // printNumber(x) outputs "x", where x is an integer. 
    public void Number(Action<int> printNumber) {
        for (int i = 1; i <= n; ++i)
        {
            if (i % 5 != 0 && i % 3 != 0)
            {
                printNumber(i);
            } 

            barrier.SignalAndWait();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int n = 15;
        FizzBuzz fizzBuzz = new FizzBuzz(n);

        Thread threadA = new Thread(() => fizzBuzz.Fizz(() => Console.Write("fizz ")));
        Thread threadB = new Thread(() => fizzBuzz.Buzz(() => Console.Write("buzz ")));
        Thread threadC = new Thread(() => fizzBuzz.Fizzbuzz(() => Console.Write("fizzbuzz ")));
        Thread threadD = new Thread(() => fizzBuzz.Number(x => Console.Write($"{x} ")));

        threadA.Start();
        threadB.Start();
        threadC.Start();
        threadD.Start();

        threadA.Join();
        threadB.Join();
        threadC.Join();
        threadD.Join();
    }
}
