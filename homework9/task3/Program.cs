class Program
{
    public static void SortStringsA(string[] strings)
    {
        List<Task> tasks = new List<Task>();

        foreach (string str in strings)
        {
            tasks.Add(Task.Run(() => {
                Thread.Sleep(str.Length * 1000);
                Console.WriteLine($"{str} {str.Length}");
            }));
        }

        Task.WaitAll(tasks.ToArray());
    }

    public static void SortStringsB(string[] strings)
    {
        List<string> sortedList = new List<string>();
        List<Task> tasks = new List<Task>();

        foreach (string str in strings)
        {
            tasks.Add(Task.Run(() =>
            {
                Thread.Sleep(str.Length * 1000);

                lock (sortedList)
                {
                    sortedList.Add(str);
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());

        lock (sortedList)
        {
            foreach (string str in sortedList)
            {
                Console.WriteLine($"{str} {str.Length}");
            }
        }
    }

    static void Main(string[] args)
    {
        string[] strings = new string[] { "Hello", "world", "This", "is", "a", "test", "string", "array" };

        Console.WriteLine("SortStringsA: ");
        SortStringsA(strings);

        Console.WriteLine("\nSortStringsB: ");
        SortStringsB(strings);
    }
}
