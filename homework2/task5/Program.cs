using System.Diagnostics;

class ListNode
{
    public int Value { get; set; }
    public ListNode? Next { get; set; }

    public ListNode(int value)
    {
        this.Value = value;
        this.Next = null;
    }
}

class SinglyLinkedList
{
    public ListNode? Head { get; private set; }

    public ListNode? GetFirst()
    {
        return Head;
    }

    public void AddFirst(int data)
    {
        ListNode newNode = new ListNode(data);
        newNode.Next = Head;
        Head = newNode;
    }

    public void AddFirst(ListNode newNode)
    {
        newNode.Next = Head;
        Head = newNode;
    }

    public void AddLast(int data)
    {
        ListNode newNode = new ListNode(data);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            ListNode current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    public void AddLast(ListNode newNode)
    {
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            ListNode current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    public void RemoveFirst()
    {
        if (Head != null)
        {
            Head = Head.Next;
        }
    }
    }

class Program
{
    static ListNode? FindIntersection(SinglyLinkedList list1, SinglyLinkedList list2)
    {
        if (list1 == null || list1.GetFirst() == null || list2 == null || list2.GetFirst() == null)
        {
            return null;
        }

        ListNode? pointer1 = list1.GetFirst();
        ListNode? pointer2 = list2.GetFirst();

        while (pointer1 != null)
        {
            while (pointer2 != null)
            {
                if (pointer1 == pointer2) {
                    return pointer1;
                }
                pointer2 = pointer2.Next;
            }
            pointer1 = pointer1.Next;
            pointer2 = list2.GetFirst();
        }

        return null;
    }

    static void Main(string[] args)
    {
        // Test Case 1: Have intersection
        SinglyLinkedList list1 = new SinglyLinkedList();
        for (int i = 1; i < 6; ++i)
        {
            list1.AddLast(i);
        }
        SinglyLinkedList list2 = new SinglyLinkedList();
        for (int i = 6; i < 8; ++i)
        {
            list2.AddLast(i);
        }

        ListNode commonNode = new ListNode(10);
        list1.AddLast(commonNode);
        list2.AddLast(commonNode);

        ListNode? intersection = FindIntersection(list1, list2);
        Debug.Assert(intersection != null && intersection.Value == 10);

        // Test Case 2: No intersection
        SinglyLinkedList list3 = new SinglyLinkedList();
        for (int i = 1; i < 5; ++i)
        {
            list3.AddLast(i);
        }
        SinglyLinkedList list4 = new SinglyLinkedList();
        for (int i = 6; i < 8; ++i)
        {
            list4.AddLast(i);
        }

        intersection = FindIntersection(list3, list4);
        Debug.Assert(intersection == null);

        // Test Case 3: Intersection in the middle of the lists
        SinglyLinkedList list5 = new SinglyLinkedList();
        for (int i = 1; i < 5; ++i)
        {
            list5.AddLast(i);
        }
        list5.AddLast(commonNode);
        for (int i = 6; i < 10; ++i)
        {
            list5.AddLast(i);
        }

        SinglyLinkedList list6 = new SinglyLinkedList();
        for (int i = 6; i < 10; ++i)
        {
            list6.AddLast(i);
        }
        list6.AddLast(commonNode);
        for (int i = 10; i < 15; ++i)
        {
            list6.AddLast(i);
        }

        intersection = FindIntersection(list5, list6);
        Debug.Assert(intersection != null && intersection.Value == 10);

        // Test Case 4: Empty Lists
        Debug.Assert(FindIntersection(new SinglyLinkedList(), new SinglyLinkedList()) == null);
        
        // Test Case 5: Lists are null
        Debug.Assert(FindIntersection(null, null) == null);
        
        Console.WriteLine("Success");
    }
}
