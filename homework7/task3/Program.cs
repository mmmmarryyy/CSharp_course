public class Program
{
    public static char[] separators = new char[] { ' ', ',', '.', ':', ';', '-', '!', '?', '"', '\'' };

    public static void GroupWordsByLength(string sentence)
    {
        var wordGroups = sentence.Split(separators, StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(word => word.Length)
            .OrderByDescending(group => group.Count())
            .ThenByDescending(group => group.Key);

        int groupNumber = 1;
        foreach (var group in wordGroups)
        {
            Console.WriteLine($"Группа {groupNumber}. Длина {group.Key}. Количество {group.Count()}");
            foreach (var word in group)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine();
            groupNumber++;
        }
    }

    public static void Main(string[] args)
    {
        GroupWordsByLength("Это что же получается: ходишь, ходишь в школу, а потом бац - вторая смена");
    }
}
