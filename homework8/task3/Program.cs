public class FooBar {
    private int n;
    
    public FooBar(int n) {
        this.n = n; 
    }

    public void Foo(Action printFoo) { 
        for (int i = 0; i < n; i++) {
            printFoo(); 
        }
    }

    public void Bar(Action printBar) {
        for (int i = 0; i < n; i++) {
            printBar(); 
        }
    } 
}

class Program
{
    static void Main(string[] args)
    {
        SemaphoreSlim _semaphoreT1 = new SemaphoreSlim(1);
        SemaphoreSlim _semaphoreT2 = new SemaphoreSlim(0);

        FooBar fooBar = new FooBar(5);

        Thread threadA = new Thread(() => {
            fooBar.Foo(() => 
                {
                    _semaphoreT1.Wait();
                    Console.Write("foo");
                    _semaphoreT2.Release();
                }
            );
        });

        Thread threadB = new Thread(() => {
            fooBar.Bar(() => 
                {
                    _semaphoreT2.Wait();
                    Console.Write("bar");
                    _semaphoreT1.Release();
                }
            );
        });

        threadA.Start();
        threadB.Start();

        threadA.Join();
        threadB.Join();
    }
}
