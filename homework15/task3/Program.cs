using System.Diagnostics;

class Program
{
    public static int MaxEnvelopes(int[][] envelopes)
    {
        if (envelopes == null || envelopes.Length == 0) return 0;

        List<Tuple<int, int>> envelopePairs = new List<Tuple<int, int>>();
        foreach (var env in envelopes)
        {
            envelopePairs.Add(new Tuple<int, int>(env[0], env[1]));
            envelopePairs.Add(new Tuple<int, int>(env[1], env[0]));
        }

        envelopePairs.Sort((a, b) => a.Item1 == b.Item1 ? b.Item2 - a.Item2 : a.Item1 - b.Item1);

        int[] dp = new int[envelopePairs.Count];
        Array.Fill(dp, 1);

        int maxLength = 1;
        for (int i = 1; i < envelopePairs.Count; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (envelopePairs[j].Item1 < envelopePairs[i].Item1 && envelopePairs[j].Item2 < envelopePairs[i].Item2)
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
            maxLength = Math.Max(maxLength, dp[i]);
        }

        return maxLength;
    }

    static void Main(string[] args)
    {
        int[][] envelopes1 = new int[][] { new int[] { 5, 4 }, new int[] { 6, 4 }, new int[] { 6, 7 }, new int[] { 2, 3 } };
        Debug.Assert(MaxEnvelopes(envelopes1) == 3);

        int[][] envelopes2 = new int[][] { new int[] { 1, 1 }, new int[] { 1, 1 }, new int[] { 1, 1 } };
        Debug.Assert(MaxEnvelopes(envelopes2) == 1);

        int[][] envelopes3 = new int[][] { new int[] { 1, 3 }, new int[] { 3, 5 }, new int[] { 6, 7 }, new int[] { 8, 9 } };
        Debug.Assert(MaxEnvelopes(envelopes3) == 4);

        int[][] envelopes4 = new int[][] { new int[] { 2, 100 }, new int[] { 300, 4 }, new int[] { 3, 200 }, new int[] { 5, 500 } };
        Debug.Assert(MaxEnvelopes(envelopes4) == 4);

        int[][] envelopes5 = new int[][] { new int[] { 1, 2 }, new int[] { 2, 3 }, new int[] { 4, 3 }, new int[] { 5, 4 }, new int[] { 5, 6 } };
        Debug.Assert(MaxEnvelopes(envelopes5) == 5);

        Console.WriteLine("Success");
    }
}
