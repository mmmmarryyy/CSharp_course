public class H2O {
    public H2O() {
    }

    public void Hydrogen(Action releaseHydrogen) {
        // releaseHydrogen() outputs "H". Do not change or remove this line.
        releaseHydrogen(); 
    }

    public void Oxygen(Action releaseOxygen) {
        // releaseOxygen() outputs "O". Do not change or remove this line.
        releaseOxygen(); 
    }
}

public class WaterSimulator
{
    private readonly H2O _h2o;
    private readonly SemaphoreSlim _hydrogenSemaphore;
    private readonly SemaphoreSlim _oxygenSemaphore;
    private readonly object _lockObject;
    private int _atomCount;

    public WaterSimulator(H2O h2o)
    {
        _h2o = h2o;
        _hydrogenSemaphore = new SemaphoreSlim(2, 2);
        _oxygenSemaphore = new SemaphoreSlim(1, 1);
        _lockObject = new object();
        _atomCount = 0;
    }

    public async Task Start(string input)
    {
        List<Task> tasks = new List<Task>();

        foreach (char c in input)
        {
            if (c == 'O') {
                tasks.Add(Task.Run(() => Oxygen()));
            } else if (c == 'H') {
                tasks.Add(Task.Run(() => Hydrogen()));
            }
        }

        await Task.WhenAll(tasks);
        Console.WriteLine(); 
    }

    public async Task Hydrogen()
    {
        await _hydrogenSemaphore.WaitAsync();
        lock (_lockObject)
        {
            handleAtomCount();
            _h2o.Hydrogen(() => Console.Write("H"));
        }
        _hydrogenSemaphore.Release();
    }

    public async Task Oxygen()
    {
        await _oxygenSemaphore.WaitAsync();
        lock (_lockObject)
        {
            handleAtomCount();
            _h2o.Hydrogen(() => Console.Write("O"));
        }
        _oxygenSemaphore.Release();
    }

    private void handleAtomCount() {
        if (++_atomCount == 3)
        {
            _atomCount = 0;
            Monitor.PulseAll(_lockObject);
        }
        else
        {
            Monitor.Wait(_lockObject);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        H2O h2o = new H2O();
        WaterSimulator simulator = new WaterSimulator(h2o);

        simulator.Start("HOH").Wait();
        simulator.Start("OOHHHH").Wait();
        simulator.Start("OOOOOOHHHHHHHHHHHH").Wait();
    }
}
