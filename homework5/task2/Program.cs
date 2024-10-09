using System.Diagnostics;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class NameLengthComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        int lengthComparison = x.Name.Length.CompareTo(y.Name.Length);
        if (lengthComparison != 0)
        {
            return lengthComparison;
        }

        return string.Compare(x.Name.Substring(0, 1), y.Name.Substring(0, 1), StringComparison.OrdinalIgnoreCase);
    }
}

class AgeComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        return x.Age.CompareTo(y.Age);
    }
}


class Program
{
    static void Main(string[] args)
    {
        List<Person> people = new List<Person>()
        {
            new Person("John", 30),
            new Person("Jane", 26),
            new Person("Alice", 20),
            new Person("Bob", 25),
            new Person("David", 35)
        };

        Console.WriteLine("Sort by name length:");
        people.Sort(new NameLengthComparer());
        people.ForEach(person => Console.WriteLine($"{person.Name} ({person.Age})"));

        Console.WriteLine("\nSort by age:");
        people.Sort(new AgeComparer());
        people.ForEach(person => Console.WriteLine($"{person.Name} ({person.Age})"));

        Console.WriteLine("Success");
    }
}
