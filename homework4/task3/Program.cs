using System.Diagnostics;

public class MultiStack<T>
{
    private readonly int _stackSize;
    private readonly List<Stack<T>> _stacks = new List<Stack<T>>();

    public MultiStack(int stackSize)
    {
        _stackSize = stackSize;
    }

    public void Push(T value)
    {
        if (_stacks.Count == 0 || _stacks[_stacks.Count-1].Count == _stackSize)
        {
            _stacks.Add(new Stack<T>());
        }

        _stacks[_stacks.Count-1].Push(value);
    }

    public T Pop()
    {
        if (_stacks.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        T value = _stacks[_stacks.Count-1].Pop();

        if (_stacks[_stacks.Count-1].Count == 0)
        {
            _stacks.RemoveAt(_stacks.Count-1);
        }

        return value;
    }
}

class Program
{
    static void Main(string[] args)
    {
        MultiStack<int> multiStack = new MultiStack<int>(3);

        multiStack.Push(50);
        multiStack.Push(40);
        multiStack.Push(30);
        multiStack.Push(20);
        multiStack.Push(10);

        Debug.Assert(multiStack.Pop() == 10);
        Debug.Assert(multiStack.Pop() == 20);
        Debug.Assert(multiStack.Pop() == 30);
        Debug.Assert(multiStack.Pop() == 40);
        Debug.Assert(multiStack.Pop() == 50);
        
        try 
        {
            multiStack.Pop();
            Debug.Fail("no exception thrown");
        }
        catch (Exception ex)
        {
            Debug.Assert(ex is InvalidOperationException);
            Debug.Assert(ex.Message == "Stack is empty");
        }

        Console.WriteLine("Success");
    }
}
