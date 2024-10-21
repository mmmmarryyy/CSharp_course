using System.Diagnostics;

public class Program
{
    public static List<string> Bucketize(string phrase, int n)
    {
        return phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(w => w.Trim())
            .Aggregate(new List<string>(), (buckets, word) =>
                {
                    if (buckets.Count == 0 || buckets.Last().Length + word.Length + 1 > n)
                    {
                        buckets.Add(word);
                    }
                    else
                    {
                        buckets[buckets.Count - 1] += $" {word}";
                    }
                    return buckets;
                }
            );
    }

    public static void Main(string[] args)
    {
        Debug.Assert(Bucketize("она продает морские раковины у моря", 16).SequenceEqual(new List<string> { "она продает", "морские раковины", "у моря" }));
        Debug.Assert(Bucketize("мышь прыгнула через сыр", 8).SequenceEqual(new List<string> { "мышь", "прыгнула", "через", "сыр" }));
        Debug.Assert(Bucketize("волшебная пыль покрыла воздух", 15).SequenceEqual(new List<string> { "волшебная пыль", "покрыла воздух" }));
        Debug.Assert(Bucketize("a b   c d   e    ", 2).SequenceEqual(new List<string> { "a", "b", "c", "d", "e" }));
        Debug.Assert(Bucketize("очень короткая фраза", 20).SequenceEqual(new List<string> { "очень короткая фраза" }));

        Console.WriteLine("Success");
    }
}
