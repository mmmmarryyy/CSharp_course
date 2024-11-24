public class BirdHouse
{
    private readonly int _initialFoodPortions;
    private int _currentFoodPortions;
    private readonly int _numberOfChicks;
    private readonly SemaphoreSlim _foodSemaphore;
    private readonly Random _random = new();

    public BirdHouse(int numberOfChicks, int initialFoodPortions)
    {
        _initialFoodPortions = initialFoodPortions;
        _currentFoodPortions = initialFoodPortions;
        _foodSemaphore = new SemaphoreSlim(initialFoodPortions, initialFoodPortions);
        _numberOfChicks = numberOfChicks;
    }

    public async Task RunSimulationAsync()
    {
        var chickTasks = new Task[_numberOfChicks];
        for (int i = 1; i <= _numberOfChicks; i++)
        {
            chickTasks[i-1] = ChickTask(i);
        }

        var cts = new CancellationTokenSource();
        
        try
        {
            await Task.WhenAll(chickTasks);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Simulations stopped.");
        }
        finally
        {
            cts.Dispose();
        }

    }

    private async Task ChickTask(int chickId)
    {
        while (true)
        {
            await _foodSemaphore.WaitAsync();

            bool flag = false;
            lock (this) 
            {
                _currentFoodPortions--;
                Console.WriteLine($"[CHICK {chickId}]: eat, {_currentFoodPortions} portions of food left.");
                flag = (_currentFoodPortions == 0);
            }

            if (flag)
            {
                Console.WriteLine($"[CHICK {chickId}]: calls mother cause there is no food left");
                await MotherRefillsBowlAsync();
            }

            await Task.Delay(_random.Next(250, 1500)); 
        }
    }

    private async Task MotherRefillsBowlAsync()
    {
        lock (this) 
        {
            _currentFoodPortions = _initialFoodPortions;
            _foodSemaphore.Release(_initialFoodPortions);
            Console.WriteLine("[MOTHER]: fills the bowl");
        }
    }
}


public class Program
{
    public static async Task Main(string[] args)
    {
        var birdHouse = new BirdHouse(9, 15);
        await birdHouse.RunSimulationAsync();
    }
}
