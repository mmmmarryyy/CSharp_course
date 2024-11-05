class Program
{
    static async Task Main(string[] args)
    {
        const int queueCapacity = 3;
        int[] queue = new int[queueCapacity];
        int queueSize = 0;

        var sync = new SemaphoreSlim(0);

        Task hairdresserTask = Task.Run(async () =>
        {
            while (true)
            {
                await sync.WaitAsync();

                if (queueSize > 0)
                {
                    int visitor = queue[0];
                    Console.WriteLine($"Hairdresser took visitor {visitor}, still waiting in line {queueSize - 1} visitors");
                    for (int i = 0; i < queueSize - 1; i++)
                    {
                        queue[i] = queue[i + 1];
                    }
                    queueSize--;

                    await Task.Delay(new Random().Next(500, 1500));
                    Console.WriteLine($"Hairdresser end work with visitor {visitor}");
                }
            }
        });

        Task visitorsTask = Task.Run(async () =>
        {
            int visitorId = 0;
            while (true)
            {
                await Task.Delay(new Random().Next(500, 1000));

                if (queueSize < queueCapacity)
                {
                    queue[queueSize++] = visitorId;
                    sync.Release();
                    Console.WriteLine($"New visitor in queue: {visitorId}");
                }
                else
                {
                    Console.WriteLine($"Visitor {visitorId} don't find free places in queue");
                }
                visitorId++;
            }
        });

        await Task.WhenAll(hairdresserTask, visitorsTask);
    }
}