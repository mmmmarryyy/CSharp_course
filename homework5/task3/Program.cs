using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class MyLinkedList<T> : IEnumerable<T>
{
    private Node<T> _head;
    private Node<T> _tail;
    public int Count { get; private set; }

    public MyLinkedList()
    {
        _head = null;
        _tail = null;
        Count = 0;
    }

    public void Add(T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            _tail = _tail.Next;
        }

        Count++;
    }

    public bool Remove(T value)
    {
        if (_head == null)
        {
            return false;
        }

        if (_head.Value.Equals(value))
        {
            _head = _head.Next;
            if (_head == null)
            {
                _tail = null;
            }
            Count--;
            return true;
        }

        Node<T> current = _head;
        while (current.Next != null)
        {
            if (current.Next.Value.Equals(value))
            {
                current.Next = current.Next.Next;
                if (current.Next == null)
                {
                    _tail = current;
                }
                Count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T> current = _head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyLinkedList<string> list = new MyLinkedList<string>();
        list.Add("Apple");
        list.Add("Banana");
        list.Add("Cherry");

        Console.WriteLine("List: " + string.Join(" ", list));
        Console.WriteLine($"Elements in list = {list.Count}\n");
        Debug.Assert(list.Count == 3);

        Console.WriteLine("Try to delete 'Banana':");
        var removeStatus = list.Remove("Banana");
        Console.WriteLine($"Element removed status: {removeStatus}\n");
        Debug.Assert(removeStatus == true);

        Console.WriteLine("Try to delete 'Banana' again:");
        removeStatus = list.Remove("Banana");
        Console.WriteLine($"Element removed status: {removeStatus}\n");
        Debug.Assert(removeStatus == false);

        Console.WriteLine("List after delete: " + string.Join(" ", list));
        Console.WriteLine($"Elements in list = {list.Count}\n");
        Debug.Assert(list.Count == 2);

        Console.WriteLine("Success");
    }
}
