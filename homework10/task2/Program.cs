class Animal
{
    private static int counter = 0;
    public int X { get; set; }
    public int Y { get; set; }
    public int id { get; init; }

    public Animal(int x, int y)
    {
        X = x;
        Y = y;
        id = counter++;
    }
}

class Program
{
    static int N = 10;
    static char[,] field = new char[N, N];
    static Random rnd = new Random();
    static List<Animal> sheepList = new List<Animal>();
    static Animal wolf;
    static List<Tuple<int, int>> directions = new List<Tuple<int, int>> {
        new Tuple<int, int>(-1, -1),
        new Tuple<int, int>(-1, 0),
        new Tuple<int, int>(-1, 1),
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(0, 1),
        new Tuple<int, int>(1, -1),
        new Tuple<int, int>(1, 0),
        new Tuple<int, int>(1, 1)
    };

    static async Task Main(string[] args)
    {
        InitField();

        wolf = new Animal(rnd.Next(N), rnd.Next(N));
        for (int i = 0; i < 5; i++)
        {
            sheepList.Add(new Animal(rnd.Next(N), rnd.Next(N)));
        }

        List<Task> tasks = new List<Task>();
        foreach (var sheep in sheepList)
        {
            tasks.Add(SheepMove(sheep));
        }
        tasks.Add(WolfMove());

        await Task.WhenAll(tasks);
    }

    static void InitField()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                field[i, j] = '.';
            }
        }
    }

    static void PrintField()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(field[i, j]);
            }
            Console.WriteLine();
        }
    }

    static Tuple<int,int> getNewCoordinates(int x, int y) {
        List<Tuple<int, int>> filteredDirections = directions.Where(
            direction => 
                x + direction.Item1 >= 0 &&
                x + direction.Item1 < N &&
                y + direction.Item2 >= 0 &&
                y + direction.Item2 < N
        ).ToList();

        Tuple<int, int> direction = filteredDirections[rnd.Next(filteredDirections.Count)];

        return new Tuple<int, int>(x + direction.Item1, y + direction.Item2);
    }

    static async Task WolfMove()
    {
        while (true)
        {
            await Task.Delay(rnd.Next(500, 1500));

            Tuple<int, int> newCoordinates = getNewCoordinates(wolf.X, wolf.Y);

            for (int i = sheepList.Count - 1; i >= 0; i--)
            {
                if (
                    newCoordinates.Item1 == sheepList[i].X && 
                    newCoordinates.Item2 == sheepList[i].Y
                )
                {
                    sheepList.Remove(sheepList[i]);
                }
            }

            wolf.X = newCoordinates.Item1;
            wolf.Y = newCoordinates.Item2;

            UpdateField();
        }
    }

    static async Task SheepMove(Animal sheep)
    {
        while (true)
        {
            await Task.Delay(rnd.Next(1000, 3000));

            Tuple<int, int> newCoordinates = getNewCoordinates(sheep.X, sheep.Y);

            foreach (var otherSheep in sheepList)
            {
                if 
                (
                    otherSheep != sheep && 
                    newCoordinates.Item1 == otherSheep.X && 
                    newCoordinates.Item2 == otherSheep.Y
                )
                {
                    sheepList.Add(new Animal(rnd.Next(0, N), rnd.Next(0, N)));
                    break;
                }
            }

            sheep.X = newCoordinates.Item1;
            sheep.Y = newCoordinates.Item2;

            if (sheep.X == wolf.X && sheep.Y == wolf.Y) {
                sheepList.Remove(sheep);
            }

            UpdateField();
        }
    }

    static void UpdateField()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                field[i, j] = '.';
            }
        }

        field[wolf.Y, wolf.X] = 'W';
        foreach (var sheep in sheepList)
        {
            if (field[sheep.Y, sheep.X] == 'S') 
            {
                field[sheep.Y, sheep.X] = 's';
            } 
            else 
            {
                field[sheep.Y, sheep.X] = 'S';
            }
        }

        Console.Clear();
        PrintField();
    }
}
