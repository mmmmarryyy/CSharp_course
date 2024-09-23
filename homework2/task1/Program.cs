using System.Diagnostics;

public class OpenAddressingHashMap<TKey, TValue>
{
    private class KeyValuePair
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    private KeyValuePair[] _table;
    private int _size;
    private int _count;
    private Func<TKey, uint> _hashFunction;
    private const int DefaultCapacity = 10;
    private const double LoadFactor = 0.75;

    public OpenAddressingHashMap(Func<TKey, uint> hashFunction)
    {
        _table = new KeyValuePair[DefaultCapacity];
        _size = DefaultCapacity;
        _count = 0;
        _hashFunction = hashFunction;
    }

    public void Set(TKey key, TValue value)
    {
        if (_count / (double)_size >= LoadFactor)
        {
            Resize();
        }

        int index = GetIndex(key);
        while (_table[index] != null)
        {
            if (_table[index].Key.Equals(key))
            {
                _table[index].Value = value;
                return;
            }
            index = (index + 1) % _size;
        }

        _table[index] = new KeyValuePair(key, value);
        _count++;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetIndex(key);
        while (_table[index] != null)
        {
            if (_table[index].Key.Equals(key))
            {
                value = _table[index].Value;
                return true;
            }
            index = (index + 1) % _size;
        }

        value = default;
        return false;
    }

    public bool ContainsKey(TKey key)
    {
        TValue temp;
        return TryGetValue(key, out temp);
    }

    public void Remove(TKey key)
    {
        int index = GetIndex(key);
        while (_table[index] != null)
        {
            if (_table[index].Key.Equals(key))
            {
                _table[index] = null;
                _count--;
                return;
            }
            index = (index + 1) % _size;
        }
    }

    private int GetIndex(TKey key)
    {
        return (int)(_hashFunction(key) % _size);
    }

    private void Resize()
    {
        KeyValuePair[] oldTable = _table;
        _size *= 2;
        _table = new KeyValuePair[_size];
        _count = 0;

        foreach (KeyValuePair pair in oldTable)
        {
            if (pair != null)
            {
                Set(pair.Key, pair.Value);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Test 1: Set and get
        OpenAddressingHashMap<string, int> hashMap = new OpenAddressingHashMap<string, int>(s => (uint)s.GetHashCode());
        hashMap.Set("Alice", 1);
        hashMap.Set("Bob", 2);
        hashMap.Set("Charlie", 3);

        Debug.Assert(hashMap.TryGetValue("Alice", out int value) && value == 1);
        Debug.Assert(hashMap.TryGetValue("Bob", out value) && value == 2);
        Debug.Assert(hashMap.TryGetValue("Charlie", out value) && value == 3);

        // Test 2: Set existing key
        hashMap = new OpenAddressingHashMap<string, int>(s => (uint)s.GetHashCode());
        hashMap.Set("Alice", 1);
        hashMap.Set("Alice", 4);

        Debug.Assert(hashMap.TryGetValue("Alice", out value) && value == 4);

        // Test 3: Contains key
        hashMap = new OpenAddressingHashMap<string, int>(s => (uint)s.GetHashCode());
        hashMap.Set("Alice", 1);
        hashMap.Set("Bob", 2);

        Debug.Assert(hashMap.ContainsKey("Alice"));
        Debug.Assert(hashMap.ContainsKey("Bob"));
        Debug.Assert(!hashMap.ContainsKey("Charlie"));

        // Test 4: Remove key
        hashMap = new OpenAddressingHashMap<string, int>(s => (uint)s.GetHashCode());
        hashMap.Set("Alice", 1);
        hashMap.Set("Bob", 2);

        hashMap.Remove("Bob");

        Debug.Assert(!hashMap.ContainsKey("Bob"));
        Debug.Assert(hashMap.TryGetValue("Alice", out value) && value == 1);

        // Test 5: Resize
        hashMap = new OpenAddressingHashMap<string, int>(s => (uint)s.GetHashCode());
        for (int i = 0; i < 15; i++)
        {
            hashMap.Set(i.ToString(), i);
        }

        Debug.Assert(hashMap.ContainsKey("9"));

        // Test 6: Collision handling
        hashMap = new OpenAddressingHashMap<string, int>(s => (uint)s.Length);
        hashMap.Set("a", 1);
        hashMap.Set("b", 2);

        Debug.Assert(hashMap.TryGetValue("a", out value) && value == 1);
        Debug.Assert(hashMap.TryGetValue("b", out value) && value == 2);

        Console.WriteLine("Success");
    }
}

