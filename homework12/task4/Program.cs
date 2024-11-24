public enum Direction { North, South }

public class BridgeController
{
    private Barrier _barrier;
    private Direction _currentDirection;
    private int _northCarsCounter = 0;
    private int _southCarsCounter = 0;
    private readonly object _lock = new object();
    private readonly Random _random = new();

    public async Task RunSimulationAsync(int numberOfCars)
    {
        _barrier = new Barrier(numberOfCars);

        var tasks = new Task[numberOfCars];

        for (int i = 1; i <= numberOfCars; i++)
        {
            tasks[i-1] = CrossBridgeAsync(_random.Next(2) == 0 ? Direction.North : Direction.South, i);
        }

        await Task.WhenAll(tasks);
    }

    private async Task CrossBridgeAsync(Direction direction, int carId)
    {
        await Task.Run(() => _barrier.SignalAndWait());

        lock (_lock)
        {
            while (_southCarsCounter + _northCarsCounter != 0 &&
                _currentDirection != direction && 
                ((_currentDirection == Direction.South && _southCarsCounter > 0) || 
                (_currentDirection == Direction.North && _northCarsCounter > 0))
            )
            {
                Monitor.Wait(_lock);
            }

            if (direction == Direction.North)
            {
                _northCarsCounter++;
            }
            else
            {
                _southCarsCounter++;
            }
            _currentDirection = direction;
            Console.WriteLine($"[CAR {carId}]: start move to {direction}");

        }

        await Task.Delay(_random.Next(150, 200));

        lock (_lock)
        {
            if (direction == Direction.North)
            {
                _northCarsCounter--;
            }
            else
            {
                _southCarsCounter--;
            }

            Console.WriteLine($"[CAR {carId}]: end move to {direction}");
            Monitor.PulseAll(_lock);
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var bridge = new BridgeController();
        await bridge.RunSimulationAsync(35);
    }
}
