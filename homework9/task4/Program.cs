public class MultithreadedFileProcessor
{
    private readonly string _inputDirectory;
    private readonly string _outputFile;
    private readonly int _threadCount;

    public MultithreadedFileProcessor(string inputDirectory, string outputFile, int threadCount)
    {
        _inputDirectory = inputDirectory;
        _outputFile = outputFile;
        _threadCount = threadCount;
    }

    public void ProcessFiles()
    {
        var files = Directory.GetFiles(_inputDirectory, "*.txt");
        var fileBatches = DivideFiles(files, _threadCount);
        var tasks = fileBatches.Select(batch => Task.Run(() => ProcessBatch(batch))).ToArray();

        Task.WaitAll(tasks);

        double totalResult = tasks.Sum(task => task.Result);

        File.WriteAllText(_outputFile, totalResult.ToString());
    }

    private List<string[]> DivideFiles(string[] files, int threadCount)
    {
        var fileBatches = new List<string[]>();
        var batchSize = files.Length / threadCount;

        for (int i = 0; i < threadCount; i++)
        {
            var startIndex = i * batchSize;
            var endIndex = (i == threadCount - 1) ? files.Length : startIndex + batchSize;
            var batch = files[startIndex..endIndex];

            fileBatches.Add(batch);
        }

        return fileBatches;
    }

    private double ProcessBatch(string[] files)
    {
        double batchResult = 0;

        foreach (var file in files)
        {
            string[] lines = File.ReadAllLines(file);
            int action = int.Parse(lines[0]);
            double[] numbers = lines[1].Split(' ').Select(double.Parse).ToArray();

            switch (action)
            {
                case 1:
                    batchResult += numbers.Sum();
                    break;
                case 2:
                    batchResult += numbers.Aggregate((a, b) => a * b);
                    break;
                case 3:
                    batchResult += numbers.Sum(x => x * x);
                    break;
                default:
                    Console.WriteLine($"Unknown action: {action}");
                    break;
            }
        }

        return batchResult;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string inputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine("Data directory doesn't exist");
        }
        else 
        {
            string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "out.dat");
            int threadCount = 4;

            var processor = new MultithreadedFileProcessor(inputDirectory, outputFile, threadCount);
            processor.ProcessFiles();

            Console.WriteLine("End.");
        }
    }
}