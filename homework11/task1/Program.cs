public class ZeroEvenOdd { 
    private int n;
    private State state = State.Zero;
    private object lockObject = new object();

    public ZeroEvenOdd(int n) {
        this.n = n; 
    }

    // printNumber(x) outputs "x", where x is an integer. 
    public void Zero(Action<int> printNumber) {
        for (int i = 1; i <= n; i++) {
            lock (lockObject) {
                while (state != State.Zero) {
                    Monitor.Wait(lockObject);
                }

                printNumber(0);
                state = (i % 2 == 0) ? State.Even : State.Odd;
                Monitor.PulseAll(lockObject);
            }
        }
    }

    public void Even(Action<int> printNumber) {
        for (int i = 2; i <= n; i += 2) {
            lock (lockObject) {
                while (state != State.Even) {
                    Monitor.Wait(lockObject);
                }

                printNumber(i);
                state = State.Zero;
                Monitor.PulseAll(lockObject);
            }
        }
    }

    public void Odd(Action<int> printNumber) {
        for (int i = 1; i <= n; i += 2) {
            lock (lockObject) {
                while (state != State.Odd) {
                    Monitor.Wait(lockObject);
                }

                printNumber(i);
                state = State.Zero;
                Monitor.PulseAll(lockObject);
            }
        }
    }

    private enum State {
        Zero,
        Even,
        Odd
    }
}

class Program {
    static void Main(string[] args) {
        ZeroEvenOdd zeroEvenOdd = new ZeroEvenOdd(9);
        
        Task zeroTask = Task.Run(() => zeroEvenOdd.Zero(Console.WriteLine));
        Task evenTask = Task.Run(() => zeroEvenOdd.Even(Console.WriteLine));
        Task oddTask = Task.Run(() => zeroEvenOdd.Odd(Console.WriteLine));

        Task.WaitAll(zeroTask, evenTask, oddTask);
    }
}
