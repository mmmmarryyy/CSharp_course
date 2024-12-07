using System.Diagnostics;

class Program
{
    public static string LongestDupSubstring(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return "";
        }

        int n = s.Length;
        string longestDup = "";

        for (int len = n - 1; len >= 1; len--)
        {
            HashSet<string> substrings = new HashSet<string>();
            for (int i = 0; i <= n - len; i++)
            {
                string sub = s.Substring(i, len);
                if (substrings.Contains(sub))
                {
                    longestDup = sub;
                    return longestDup;
                }
                substrings.Add(sub);
            }
        }

        return longestDup;
    }

    static void Main(string[] args)
    {
        Debug.Assert(LongestDupSubstring("mask4cask") == "ask");
        Debug.Assert(LongestDupSubstring("abcd") == "");
        Debug.Assert(LongestDupSubstring("") == "");
        Debug.Assert(LongestDupSubstring(null) == "");
        Debug.Assert(LongestDupSubstring("abacaba") == "aba");
        Debug.Assert(LongestDupSubstring("aaaaa") == "aaaa");
        Debug.Assert(LongestDupSubstring("banana") == "ana");
        Debug.Assert(LongestDupSubstring("abcabcabc") == "abcabc");
        Debug.Assert(LongestDupSubstring("abcabcab") == "abcab");
        Debug.Assert(LongestDupSubstring("aabaabaab") == "aabaab");
        Debug.Assert(LongestDupSubstring("thisisateststring") == "is");
        Debug.Assert(LongestDupSubstring("12345678901234") == "1234");
        Debug.Assert(LongestDupSubstring("!!!!") == "!!!");
        Debug.Assert(LongestDupSubstring("xyxyxy") == "xyxy");

        Console.WriteLine("Success");
    }
}
