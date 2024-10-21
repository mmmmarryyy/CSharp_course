using System.Diagnostics;

public class Program
{
    public static List<(int, int, int)> ThreeSum(int[] nums)
    {
        return (
                from i in Enumerable.Range(0, nums.Length)
                from j in Enumerable.Range(i + 1, nums.Length - i - 1)
                from k in Enumerable.Range(j + 1, nums.Length - j - 1)
                select (i, j, k)
            ).AsEnumerable()
            .SelectMany(
                indexes => 
                    new List<(int, int, int)> 
                    {
                        (nums[indexes.Item1], nums[indexes.Item2], nums[indexes.Item3]), 
                        (0, nums[indexes.Item1], nums[indexes.Item2])
                    }
            )
            .Distinct()
            .Where(triplet => (triplet.Item1 + triplet.Item2 + triplet.Item3 == 0))
            .OrderBy(triplet => triplet.Item1)
            .ToList();
    }

    public static void Main(string[] args)
    {
        Debug.Assert(ThreeSum(new int[] { 0, 1, -1, -1, 2 }).SequenceEqual(new List<(int, int, int)> { ( -1, -1, 2 ), ( 0, 1, -1 ) }));
        Debug.Assert(ThreeSum(new int[] { 0, 0, 0, 5, -5 }).SequenceEqual(new List<(int, int, int)> { ( 0, 0, 0 ), ( 0, 5, -5 ) }));
        Debug.Assert(ThreeSum(new int[] { 1, 2, 3 }).SequenceEqual(new List<(int, int, int)>()));
        Debug.Assert(ThreeSum(new int[1]).SequenceEqual(new List<(int, int, int)>()));

        Console.WriteLine("Success");
    }
}