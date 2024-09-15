class Program
{
    private static Random rnd = new Random();
    private static string chars = "abcdefghijklmnopqrstuvwxyz";

    static string generatePassword() {

        int sizeOfPassword = rnd.Next(5, 20);

        List<char> str = new List<char>(sizeOfPassword);
        for (int i = 0; i < sizeOfPassword; i++)
        {
            str.Add(chars[rnd.Next(26)]);
        }

        List<int> indexes = Enumerable.Range(0, sizeOfPassword).ToList();

        int current_index = indexes[rnd.Next(0, indexes.Count)];
        str[current_index] = '_';
        indexes.Remove(current_index);

        int numberOfUpperCase = rnd.Next(2, Math.Max(3, indexes.Count / 2));
        for (int i = 0; i < numberOfUpperCase; i++)
        {
            current_index = indexes[rnd.Next(0, indexes.Count)];
            str[current_index] = char.ToUpper(chars[rnd.Next(chars.Length)]);
            indexes.Remove(current_index);
        }

        List<int> numbersIndexes = new List<int>(rnd.Next(1, Math.Min(5, indexes.Count / 2)));
        for (int i = 0; i < numbersIndexes.Capacity; i++)
        {
            int possibleNumberIndex = indexes[rnd.Next(0, indexes.Count)];

            while (numbersIndexes.Contains(possibleNumberIndex-1) || numbersIndexes.Contains(possibleNumberIndex+1))
            {
                possibleNumberIndex = indexes[rnd.Next(0, indexes.Count)];
            }

            numbersIndexes.Add(possibleNumberIndex);
            str[numbersIndexes[i]] = rnd.Next(10).ToString().ToCharArray()[0];
            indexes.Remove(numbersIndexes[i]);
        }

        return new string(str.ToArray());
    }

    static void Main(string[] args)
    {
        Console.WriteLine(generatePassword());
    }
}
