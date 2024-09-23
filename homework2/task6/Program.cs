using System.Diagnostics;

class Program
{
    static int Solve(List<int> heights)
    {
        int l = 0;
        int r = heights.Count-1;
        int maxLeft = heights[l];
        int maxRight = heights[r];
        int sum = 0;

        while (l < r) 
        {
            if (heights[l] < heights[r]) 
            {
                int possibleNumberOfWater = Math.Min(maxLeft, maxRight) - heights[l];
                sum += (possibleNumberOfWater > 0) ? possibleNumberOfWater : 0;
                l++;
                maxLeft = Math.Max(maxLeft, heights[l]);
            } 
            else 
            {
                int possibleNumberOfWater = Math.Min(maxLeft, maxRight) - heights[r];
                sum += (possibleNumberOfWater > 0) ? possibleNumberOfWater : 0;
                r--;
                maxRight = Math.Max(maxRight, heights[r]);
            }
        }

        return sum;
    }

    static void Main()
    {
        Debug.Assert(Solve(new List<int>() { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }) == 6);
        Debug.Assert(Solve(new List<int>() { 4, 2, 0, 3, 2, 5 }) == 9);
        Console.WriteLine("Success");
    }
}
