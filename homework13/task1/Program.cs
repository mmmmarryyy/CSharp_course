public class MyDisposable : IDisposable
{
    public int Value { get; set; }
    private bool disposedValue = false;
    
    public void Dispose()
    {
        if (!disposedValue)
        {
            Console.WriteLine($"Disposing MyDisposable with value: {Value}");
            disposedValue = true;
            GC.SuppressFinalize(this);
        }
    }

    ~MyDisposable() {
        Dispose();
    }
}

public class CachedItem<T> : IDisposable where T : IDisposable
{
    public readonly Guid Id = Guid.NewGuid();
    public T Data { get; set; }
    public DateTimeOffset LastAccessed { get; set; }

    public void Dispose()
    {
        ((IDisposable)Data)?.Dispose();
    }

    ~CachedItem() {
        Dispose();
    }
}


public class MyCache<T>: IDisposable where T : IDisposable
{
    private readonly int _capacity;
    private readonly TimeSpan _inactivityThreshold;
    private readonly Dictionary<Guid, CachedItem<T>> _items = new Dictionary<Guid, CachedItem<T>>();
    Thread _thWaitForFullGC;
    private bool _checkForNotify = true;
    private bool _disposed = false;
    private readonly object _lock = new object();


    public MyCache(int capacity, TimeSpan inactivityThreshold)
    {
        _capacity = capacity;
        _inactivityThreshold = inactivityThreshold;
        GC.RegisterForFullGCNotification(10, 10);
        _thWaitForFullGC = new Thread(WaitForFullGCProc);
        _thWaitForFullGC.Start();
    }

    public Guid Add(T item)
    {
        lock (_lock)
        {
            if (_items.Count >= _capacity)
            {
                Cleanup();
            }

            var cachedItem = new CachedItem<T> { Data = item, LastAccessed = DateTimeOffset.Now };
            _items.Add(cachedItem.Id, cachedItem);
            return cachedItem.Id;
        }
    }

    public T Get(Guid id)
    {
        lock (_lock)
        {
            if (_items.TryGetValue(id, out var cachedItem))
            {
                cachedItem.LastAccessed = DateTimeOffset.Now;
                return cachedItem.Data;
            }
            return default;
        }
    }

    private void Cleanup()
    {
        lock (_lock)
        {
            Console.WriteLine("Inside Cleanup function");
            var cutoffTime = DateTimeOffset.Now - _inactivityThreshold;
            var itemsToRemove = _items.Keys.Where(key => _items[key].LastAccessed < cutoffTime).ToList();

            foreach (var id in itemsToRemove)
            {
                _items[id].Dispose();
                _items.Remove(id);
            }
        }
    }

    private void WaitForFullGCProc()
    {
        while (true)
        {
            while (_checkForNotify)
            {
                GCNotificationStatus s = GC.WaitForFullGCApproach();
                if (s == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("GC Notification raised.");
                    this.Cleanup();
                }
                else if (s == GCNotificationStatus.Canceled)
                {
                    Console.WriteLine("GC Notification cancelled.");
                    break;
                }
                else
                {
                    Console.WriteLine("GC Notification not applicable.");
                    break;
                }
            }

            Thread.Sleep(500);
            if (!_checkForNotify)
            {
                break;
            }
        }
    }

    public void Dispose()
    {
        Console.WriteLine("inside Dispose for myCache");
        if (_disposed) return;
        _disposed = true;
        _checkForNotify = false;
        GC.CancelFullGCNotification();
        _thWaitForFullGC?.Join();

        foreach (var item in _items.Values)
        {
            item.Dispose();
        }
        _items.Clear();
    }

    ~MyCache() {
        Console.WriteLine("inside destructor for myCache");
        Dispose();
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        SimpleTest();

        await RunStressTest();
        Console.WriteLine("Program finished.");
    }

    public static void SimpleTest()
    {
        Console.WriteLine("Starting simple test...");
        using (MyCache<MyDisposable> myCache = new MyCache<MyDisposable>(2, TimeSpan.FromMilliseconds(100))) {
            var id1 = myCache.Add(new MyDisposable { Value = 1 });
            var id2 = myCache.Add(new MyDisposable { Value = 2 });
            Thread.Sleep(200);
            var id3 = myCache.Add(new MyDisposable { Value = 3 });
        }
        Console.WriteLine("Simple test completed.\n\n");
    }
    
    static async Task RunStressTest()
    {
        Console.WriteLine("Starting stress test...");
        var capacity = 200;
        using (MyCache<MyDisposable> cache = new MyCache<MyDisposable>(capacity, TimeSpan.FromMilliseconds(1000))) {
            List<byte[]> data = new List<byte[]>();
            for (int i = 0; i < capacity; i++)
            {
                if (i % 100 == 0) {
                    Console.WriteLine($"i = {i}");
                }
                cache.Add(new MyDisposable { Value = i });
                data.Add(new byte[100000]);
                await Task.Delay(10);
            }

            await Task.Delay(1000);
        }
        
        Console.WriteLine("Stress test completed.");
    }
}
