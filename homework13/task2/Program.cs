using System.Diagnostics;

public class PermutationGenerator
{
    public string Permutations(string str)
    {
        List<string> permutations = new List<string>();
        GeneratePermutations(str.ToCharArray(), 0, permutations);
        return string.Join(" ", permutations.OrderBy(s => s));
    }

    private void GeneratePermutations(char[] arr, int l, List<string> permutations)
    {
        if (l == arr.Length - 1)
        {
            permutations.Add(new string(arr));
        }
        else
        {
            for (int i = l; i < arr.Length; i++)
            {
                Swap(arr, l, i);
                GeneratePermutations(arr, l + 1, permutations);
                Swap(arr, l, i);
            }
        }
    }

    private void Swap(char[] arr, int i, int j)
    {
        char temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        PermutationGenerator generator = new PermutationGenerator();

        Debug.Assert(generator.Permutations("AB") == "AB BA");
        Debug.Assert(generator.Permutations("CD") == "CD DC");
        Debug.Assert(generator.Permutations("EF") == "EF FE");
        Debug.Assert(generator.Permutations("NOT") == "NOT NTO ONT OTN TNO TON");
        Debug.Assert(generator.Permutations("RAM") == "AMR ARM MAR MRA RAM RMA");
        Debug.Assert(generator.Permutations("YAW") == "AWY AYW WAY WYA YAW YWA");

        Console.WriteLine("Success");
    }
}
