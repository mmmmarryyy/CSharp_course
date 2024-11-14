public class MultithreadedArray
{
    private int[] _array;
    private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
    private readonly Random _random = new Random();

    public MultithreadedArray(int[] array)
    {
        _array = array;
    }

    public void Start(int threadCount)
    {
        var tasks = new List<Task>();

        for (int i = 0; i < threadCount; i++)
        {
            tasks.Add(Task.Run(() => FindMin()));
            tasks.Add(Task.Run(() => CalculateAverage()));
            tasks.Add(Task.Run(() => SortArray()));
            tasks.Add(Task.Run(() => SwapElements()));
        }

        Task.WaitAll(tasks.ToArray());
    }

    private void FindMin()
    {
        _lock.EnterReadLock();
        int min = _array[0];
        for (int i = 1; i < _array.Length; i++)
        {
            if (_array[i] < min)
            {
                min = _array[i];
            }
        }
        Console.WriteLine($"Минимальное значение: {min}");
        _lock.ExitReadLock();
    }

    private void CalculateAverage()
    {
        _lock.EnterReadLock();
        long sum = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            sum += _array[i];
        }
        double average = (double)sum / _array.Length;
        Console.WriteLine($"Среднее значение: {average}");
        _lock.ExitReadLock();
    }

    private void SortArray()
    {
        _lock.EnterWriteLock();
        Array.Sort(_array);
        Console.WriteLine($"Отсортированный массив: [{string.Join(", ", _array)}]");
        _lock.ExitWriteLock();
    }

    private void SwapElements()
    {
        _lock.EnterWriteLock();
        int index1 = _random.Next(_array.Length);
        int index2 = _random.Next(_array.Length);
        (_array[index1], _array[index2]) = (_array[index2], _array[index1]);
        Console.WriteLine($"Элементы с индексами {index1} и {index2} поменялись местами, массив: [{string.Join(", ", _array)}]");
        _lock.ExitWriteLock();
    }
}


class Program
{
    static void Main(string[] args)
    {
        int[] array = { 5, 2, 8, 1, 9, 4, 3, 7, 6 };
        MultithreadedArray multithreadedArray = new MultithreadedArray(array);
        multithreadedArray.Start(4);
    }
}
