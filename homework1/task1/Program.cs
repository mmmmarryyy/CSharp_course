using System.Diagnostics;

class HashMap
{
    private List<LinkedList<(object, object)>> _hashmap;

    public HashMap(Int32 mapSize)
    {
        this._hashmap = new List<LinkedList<(object, object)>>(mapSize);

        for (Int32 i = 0; i < mapSize; ++i)
        {
            this._hashmap.Add(new LinkedList<(object, object)>());
        }
    }

    public void Insert(object key, object value)
    {
        foreach ((object, object) node in _hashmap[System.Math.Abs(key.GetHashCode() % _hashmap.Count)])
        {
            if (node.Item1.Equals(key))
            {
                return;
            }
        }

        _hashmap[System.Math.Abs(key.GetHashCode() % _hashmap.Count)].AddFirst((key, value));
    }

    public void Remove(object key)
    {
        foreach ((object, object) node in this._hashmap[System.Math.Abs(key.GetHashCode() % _hashmap.Count)])
        {
            if (node.Item1.Equals(key))
            {
                this._hashmap[System.Math.Abs(key.GetHashCode() % _hashmap.Count)].Remove(node);
                return;
            }
        }
    }

    public object? Find(object key)
    {
        // Console.WriteLine($"key = {key}, hash = {System.Math.Abs(key.GetHashCode() % _hashmap.Count)}");
        
        foreach ((object, object) node in this._hashmap[System.Math.Abs(key.GetHashCode() % _hashmap.Count)])
        {
            if (node.Item1.Equals(key))
            {
                return node.Item2;
            }
        }

        return null;
    }
}

class Program
{
    static void Main(string[] args) 
    {
        var hashMap = new HashMap(10);
        hashMap.Insert("apple", "pu");
        hashMap.Insert("banana", "pupu");
        hashMap.Insert("cherry", "pupupu");
        hashMap.Insert("date", "pupupupu");
        hashMap.Insert("elderberry", "pux5");

        Debug.Assert(hashMap.Find("apple") == "pu");
        Debug.Assert(hashMap.Find("banana") == "pupu");
        Debug.Assert(hashMap.Find("cherry") == "pupupu");
        Debug.Assert(hashMap.Find("date") == "pupupupu");
        Debug.Assert(hashMap.Find("elderberry") == "pux5");

        hashMap.Insert("apple", "red");
        Debug.Assert(hashMap.Find("apple") == "pu");

        hashMap.Remove("banana");
        hashMap.Remove("cherry");

        Debug.Assert(hashMap.Find("banana") == null);
        Debug.Assert(hashMap.Find("cherry") == null);
        Debug.Assert(hashMap.Find("grape") == null);

        hashMap.Insert("fig", "pux6");
        hashMap.Insert("grape", "pux7");
        Debug.Assert(hashMap.Find("fig") == "pux6");
        Debug.Assert(hashMap.Find("grape") == "pux7");

        Console.WriteLine("Success");
    }
}
