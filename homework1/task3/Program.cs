using System.Diagnostics;

class StackWithMinValue {
    private Stack<(int, int)> _stack;

    public StackWithMinValue()
    {
        this._stack = new Stack<(int, int)>();
    }

    public void Push(int value)
    {
        if (this._stack.Count == 0) {
            this._stack.Push((value, value));
        } else {
            this._stack.Push((value, (value < this._stack.Peek().Item2) ? value : this._stack.Peek().Item2));
        }
    }

    public int? Pop()
    {
        if (this._stack.Count == 0) {
            return null;
        } else {
            int returnValue = this._stack.Peek().Item1;
            this._stack.Pop();
            return returnValue;
        }
    }

    public int? GetMinValue()
    {
        if (this._stack.Count == 0) {
            return null;
        } else { 
            return this._stack.Peek().Item2;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        StackWithMinValue s = new StackWithMinValue();

        Debug.Assert(s.GetMinValue() == null);

        s.Push(2);
        s.Push(4);

        Debug.Assert(s.GetMinValue() == 2);

        s.Push(1);
        s.Push(3);

        Debug.Assert(s.GetMinValue() == 1);

        Debug.Assert(s.Pop() == 3);
        Debug.Assert(s.Pop() == 1);

        Debug.Assert(s.GetMinValue() == 2);

        Debug.Assert(s.Pop() == 4);
        Debug.Assert(s.Pop() == 2);
        Debug.Assert(s.Pop() == null);
        Debug.Assert(s.GetMinValue() == null);

        Console.WriteLine("Success");
    }
}
