public class HoneyPot
{
    private int _capacity;
    private int _currentHoney;
    private object _lock = new object();

    public HoneyPot(int capacity)
    {
        _capacity = capacity;
        _currentHoney = 0;
    }

    public async Task AddHoneyAsync(int beeId)
    {
        await Task.Delay(new Random().Next(100, 3000));

        lock (_lock)
        {
            while (_currentHoney == _capacity)
            {
                Console.WriteLine($"Honey pot is full! Bee {beeId} is waiting for bear.");
                Monitor.Wait(_lock);
            }
            _currentHoney++;
            Console.WriteLine($"Bee {beeId} added honey. Current honey: " + _currentHoney);
            
            if (_currentHoney == _capacity)
            {
                Console.WriteLine($"Honey pot is full! Bee {beeId} is waking up the bear.");
                Monitor.PulseAll(_lock);
            }
        }
    }

    public async Task EatHoneyAsync()
    {
        lock (_lock)
        {
            while (_currentHoney != _capacity)
            {
                Monitor.Wait(_lock);
            }
            _currentHoney = 0;
            Console.WriteLine("Bear woke up and ate all the honey.");
            Monitor.PulseAll(_lock);
        }
    }
}

public class Bee
{
    private HoneyPot _honeyPot;
    private int _beeId;

    public Bee(HoneyPot honeyPot, int beeId)
    {
        _honeyPot = honeyPot;
        _beeId = beeId;
    }

    public async Task CollectHoneyAsync()
    {
        while (true)
        {
            await _honeyPot.AddHoneyAsync(_beeId);
        }
    }
}

public class Bear
{
    private HoneyPot _honeyPot;

    public Bear(HoneyPot honeyPot)
    {
        _honeyPot = honeyPot;
    }

    public async Task SleepAndEatAsync()
    {
        while (true)
        {
            await _honeyPot.EatHoneyAsync();
        }
    }
}

public class Program
{
    static async Task Main(string[] args)
    {
        var honeyPot = new HoneyPot(10);
        var bees = new List<Bee>();
        for (int i = 0; i < 15; i++)
        {
            bees.Add(new Bee(honeyPot, i));
        }

        var bear = new Bear(honeyPot);

        var tasks = bees.Select(bee => bee.CollectHoneyAsync()).ToList();
        tasks.Add(bear.SleepAndEatAsync());

        await Task.WhenAll(tasks);
    }
}

/*
output example:

Bee 10 added honey. Current honey: 1
Bee 1 added honey. Current honey: 2
Bee 13 added honey. Current honey: 3
Bee 9 added honey. Current honey: 4
Bee 6 added honey. Current honey: 5
Bee 0 added honey. Current honey: 6
Bee 9 added honey. Current honey: 7
Bee 14 added honey. Current honey: 8
Bee 10 added honey. Current honey: 9
Bee 2 added honey. Current honey: 10
Honey pot is full! Bee 2 is waking up the bear.
Bear woke up and ate all the honey.
Bee 1 added honey. Current honey: 1
Bee 12 added honey. Current honey: 2
Bee 9 added honey. Current honey: 3
Bee 8 added honey. Current honey: 4
Bee 0 added honey. Current honey: 5
Bee 13 added honey. Current honey: 6
Bee 12 added honey. Current honey: 7
Bee 7 added honey. Current honey: 8
Bee 3 added honey. Current honey: 9
Bee 3 added honey. Current honey: 10
Honey pot is full! Bee 3 is waking up the bear.
Bear woke up and ate all the honey.
Bee 5 added honey. Current honey: 1
Bee 6 added honey. Current honey: 2
Bee 2 added honey. Current honey: 3
Bee 10 added honey. Current honey: 4
Bee 4 added honey. Current honey: 5
Bee 1 added honey. Current honey: 6
Bee 11 added honey. Current honey: 7
Bee 3 added honey. Current honey: 8
Bee 0 added honey. Current honey: 9
Bee 14 added honey. Current honey: 10
Honey pot is full! Bee 14 is waking up the bear.
Bear woke up and ate all the honey.
Bee 10 added honey. Current honey: 1
Bee 12 added honey. Current honey: 2
Bee 7 added honey. Current honey: 3
Bee 13 added honey. Current honey: 4
Bee 0 added honey. Current honey: 5
Bee 8 added honey. Current honey: 6
Bee 6 added honey. Current honey: 7
Bee 10 added honey. Current honey: 8
Bee 9 added honey. Current honey: 9
Bee 11 added honey. Current honey: 10
Honey pot is full! Bee 11 is waking up the bear.
*/