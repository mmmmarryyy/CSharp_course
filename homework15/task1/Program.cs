using System.Reflection;

public class BlackBox
{
    private int innerValue;
    private const int DefaultValue = 0;

    
    private BlackBox(int innerValue)
    {
        this.innerValue = 0;
    }

    private BlackBox()
    {
        this.innerValue = DefaultValue;
    }

    private void Add(int addend)
    {
        this.innerValue += addend;
    }

    private void Subtract(int subtrahend)
    {
        this.innerValue -= subtrahend;
    }

    private void Multiply(int multiplier)
    {
        this.innerValue *= multiplier;
    }

    private void Divide(int divider)
    {
        this.innerValue /= divider;
    } 
}

class Program
{
    static void Main(string[] args)
    {
        var blackBoxType = typeof(BlackBox);
        var blackBox = (BlackBox)Activator.CreateInstance(blackBoxType, true);

        string input;

        while (true)
        {
            Console.WriteLine("Enter operation (or 'exit'): ");
            input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                break;
            }

            try
            {
                ExecuteOperation(blackBox, input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static void ExecuteOperation(BlackBox blackBox, string input)
    {
        var parts = input.Split('(', ')');
        if (parts.Length != 3 || parts[1].Trim() == "")
        {
            throw new ArgumentException("Invalid input format. Use 'Operation(value)'");
        }

        var operationName = parts[0].Trim();
        int value;
        try
        {
            value = int.Parse(parts[1].Trim());
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid input value. Value must be an integer.");
        }

        var type = blackBox.GetType();
        var method = type.GetMethod(operationName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (method == null)
        {
            throw new ArgumentException($"Invalid operation: {operationName}");
        }

        method.Invoke(blackBox, new object[] { value });

        var innerValueField = type.GetField("innerValue", BindingFlags.NonPublic | BindingFlags.Instance);
        int innerValue = (int)innerValueField.GetValue(blackBox);
        Console.WriteLine(innerValue);
    }
}
