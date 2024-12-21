using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class Group 
{
    public decimal GroupId { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
    
    [field: NonSerialized]
    public int StudentsCount { get; set; }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        StudentsCount = Students.Count;
    }
}

[Serializable]
class Student
{
    public decimal StudentId { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public int Age { get; set; }
    public Group Group { get; set; }
}

class Program
{
    static void PrintGroup(Group group)
    {
        Console.WriteLine("Group id: " + group.GroupId);
        Console.WriteLine("Group name: " + group.Name);
        Console.WriteLine("Students (count = " + group.StudentsCount + "): ");
        foreach (var student in group.Students)
        {
            Console.WriteLine("    {" +
                student.StudentId + ", " +
                student.FirstName + ", " +
                student.LastName + ", " +
                student.Age + ", " +
                student.Group.Name + "}");
        }
    }

    static void Main(string[] args)
    {
        var group = new Group { GroupId = decimal.Zero, Name = "group name" };
        var student0 = new Student { 
            StudentId = decimal.Zero, 
            FirstName = "firstname0",
            LastName = "lastname0", 
            Age = 20, 
            Group = group 
        };
        var student1 = new Student { 
            StudentId = decimal.One, 
            FirstName = "firstname1",
            LastName = "lastname1", 
            Age = 30, 
            Group = group 
        };
        group.Students = new List<Student> { student0, student1 };
        group.StudentsCount = 2;

        Console.WriteLine("Initial group:");
        PrintGroup(group);
        Console.WriteLine("");
        
        using (var memoryStream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, group);
            memoryStream.Position = 0;
            
            var newGroup = (Group) formatter.Deserialize(memoryStream);
            
            Console.WriteLine("New group:");
            PrintGroup(newGroup);
        }
    }
}
