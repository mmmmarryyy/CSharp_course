using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

class Lake : IEnumerable<int>
{
    private readonly List<int> _stones;

    public Lake(params int[] args)
    {
        var stones = args.OrderBy(x => x%2 == 0).ToList();
        var firstEvenIndex = stones.FindIndex(stone => stone % 2 == 0);

        if (firstEvenIndex > 0) {
            stones.Reverse(firstEvenIndex, stones.Count - firstEvenIndex);
        }

        _stones = stones;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return _stones.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    public static List<int> PrintFrogJumps(int[] stones) // return something only for debug
    {
        Lake lake = new Lake(stones);
        var jumps = new List<int>();

        foreach (int stone in lake)
        {
            jumps.Add(stone);
        }

        Console.WriteLine(string.Join(" ", jumps));
        return jumps;
    }

    static void Main(string[] args)
    {
        int[] stones1 = { 1, 2, 3, 4, 5, 6, 7, 8 };
        List<int> expectedJumpOrder1 = new List<int> { 1, 3, 5, 7, 8, 6, 4, 2 };

        Debug.Assert(PrintFrogJumps(stones1).SequenceEqual(expectedJumpOrder1));

        int[] stones2 = { 13, 23, 1, -8, 4, 9 };
        List<int> expectedJumpOrder2 = new List<int> { 13, 23, 1, 9, 4, -8 };

        Debug.Assert(PrintFrogJumps(stones2).SequenceEqual(expectedJumpOrder2));

        Console.WriteLine("Success");
    }
}
