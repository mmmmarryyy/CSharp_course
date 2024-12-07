using System.Diagnostics;

class Program
{
    public static bool DiffersByOneCharacter(string s1, string s2)
    {
        if (s1 == null || s2 == null)
        {
            throw new ArgumentNullException("Strings cannot be null.");
        }

        int len1 = s1.Length;
        int len2 = s2.Length;

        if (Math.Abs(len1 - len2) > 1)
        {
            return false;
        }

        if (len1 == len2)
        {
            int diffCount = 0;
            for (int i = 0; i < len1; i++)
            {
                if (s1[i] != s2[i])
                {
                    diffCount++;
                }
            }
            return diffCount == 1;
        }
        else
        {
            string longer = (len1 > len2) ? s1 : s2;
            string shorter = (len1 > len2) ? s2 : s1;
            int i = 0, j = 0;
            int diffCount = 0;

            while (i < longer.Length && j < shorter.Length)
            {
                if (longer[i] != shorter[j])
                {
                    diffCount++;
                    i++;
                }
                else
                {
                    i++;
                    j++;
                }
            }
            return diffCount <= 1;
        }
    }

    static void Main(string[] args)
    {
        Debug.Assert(DiffersByOneCharacter("pale", "ple"));
        Debug.Assert(DiffersByOneCharacter("pales", "pale"));
        Debug.Assert(DiffersByOneCharacter("pale", "bale"));
        Debug.Assert(DiffersByOneCharacter("pale", "pake")); 
        Debug.Assert(!DiffersByOneCharacter("pale", "bake"));
        Debug.Assert(DiffersByOneCharacter("apple", "aple"));
        Debug.Assert(DiffersByOneCharacter("apple", "apples"));
        Debug.Assert(DiffersByOneCharacter("apples", "apple"));
        Debug.Assert(DiffersByOneCharacter("abc", "abcd"));
        Debug.Assert(DiffersByOneCharacter("abcd", "abc"));
        Debug.Assert(DiffersByOneCharacter("abc", "adc")); 
        Debug.Assert(DiffersByOneCharacter("abc", "abd")); 
        Debug.Assert(DiffersByOneCharacter("abc", "axc")); 
        Debug.Assert(DiffersByOneCharacter("abc", "abce"));
        Debug.Assert(DiffersByOneCharacter("", "a"));     
        Debug.Assert(DiffersByOneCharacter("a", ""));
        Debug.Assert(!DiffersByOneCharacter("aa", ""));     
        Debug.Assert(!DiffersByOneCharacter("abc", "a"));
        Debug.Assert(DiffersByOneCharacter("abc", "abcd"));

        Console.WriteLine("Success");
    }
}
