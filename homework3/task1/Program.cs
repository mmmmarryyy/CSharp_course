namespace task1;

class Program
{
    static void Main(string[] args)
    {
        // var x = new
        // {
        //     Items = new List<int> { 1, 2, 3 }.GetEnumerator()
        // };
        // while (x.Items.MoveNext())
        //     Console.WriteLine(x.Items.Current); // бесконечный ноль

        // ОБЪЯСНЕНИЕ
        // List<T>.Enumerator - структура. При получить структуру из свойста происходит копирование (value type).
        // При попытке вызвать следующий код:
        // Console.WriteLine(ReferenceEquals(x.Items, x.Items));
        // выведется в консоль `false` и высветится warning:
        // warning CA2013: Do not pass an argument with value type 'System.Collections.Generic.List<int>.Enumerator' to 'ReferenceEquals'.
        // Due to value boxing, this call to 'ReferenceEquals' will always return 'false'.

        // Решить можно так
        var x = new
        {
            Items = new List<int> { 1, 2, 3 }
        };

        var itemsEnumerator = x.Items.GetEnumerator();

        while (itemsEnumerator.MoveNext())
            Console.WriteLine(itemsEnumerator.Current);

        // Или вот так
        var x1 = new 
        { 
            Items = ((IEnumerable<int>)new List<int> { 1, 2, 3 }).GetEnumerator() 
        };

        while (x1.Items.MoveNext())
        {
            Console.WriteLine(x1.Items.Current);
        }
    }
}
