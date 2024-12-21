class Program
{
    public static string FindFile(string fileName, string searchRoot = "/")
    {
        try
        {
            if (!Directory.Exists(searchRoot))
            {
                return null;
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);

            foreach (string item in Directory.GetFileSystemEntries(searchRoot))
            {
                if (Directory.Exists(item))
                {
                    string result = FindFile(fileName, item);
                    if (result != null)
                    {
                        return result;
                    }
                }
                else
                {
                    string itemNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                    string itemExtension = Path.GetExtension(item);

                    if (string.Equals(itemNameWithoutExtension, fileNameWithoutExtension, StringComparison.OrdinalIgnoreCase) &&
                        (string.IsNullOrEmpty(fileExtension) || string.Equals(itemExtension, fileExtension, StringComparison.OrdinalIgnoreCase)))
                    {
                        return Path.GetFullPath(item);
                    }
                }
            }
            return null;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access to the directory is denied: {searchRoot}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }

    }

    static void Main(string[] args)
    {
        string filePath = FindFile("Program", "/Users/maria.barkovskaya/Documents");

        if (filePath != null)
        {
            Console.WriteLine($"Найден файл: {filePath}");
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}
