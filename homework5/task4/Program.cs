public class Program
{
    private static char[] delimiterChars = { '\t', ' ' };

    private static List<string[]> ReadTable(string filename)
    {
        List<string[]> table = new List<string[]>();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                table.Add(line.Split(delimiterChars));
            }
        }
        return table;
    }

    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("It is necessary to provide 2 files");
            return;
        }

        string file1 = args[0];
        string file2 = args[1];

        List<string[]> table1 = ReadTable(file1);
        List<string[]> table2 = ReadTable(file2);

        Dictionary<string, List<string[]>> table1Dict = new Dictionary<string, List<string[]>>();
        
        foreach (string[] record in table1)
        {
            string key = record[0];
            if (!table1Dict.ContainsKey(key))
            {
                table1Dict[key] = new List<string[]>();
            }
            table1Dict[key].Add(record);
        }

        foreach (string[] record2 in table2)
        {
            string key = record2[0];
            if (table1Dict.ContainsKey(key))
            {
                foreach (string[] record1 in table1Dict[key])
                {
                    string[] joinedRecord = new string[record1.Length + record2.Length - 1];
                    Array.Copy(record1, 0, joinedRecord, 0, record1.Length);
                    Array.Copy(record2, 1, joinedRecord, record1.Length, record2.Length - 1);
                    Console.WriteLine(string.Join("\t", joinedRecord));
                }
            }
        }
    }
}
