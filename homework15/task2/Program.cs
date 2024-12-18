using System.Reflection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAttribute : Attribute
{
    public string Author { get; set; }
    public int Revision { get; set; }
    public string Description { get; set; }
    public string[] Reviewers { get; set; }

    public CustomAttribute(string author, int revision, string description, params string[] reviewers)
    {
        Author = author;
        Revision = revision;
        Description = description;
        Reviewers = reviewers;
    }
}


[Custom("Joe", 2, "Class to work with health data.", "Arnold", "Bernard")] 
public class HealthScore
{
    [Custom("Andrew", 3, "Method to collect health data.", "Sam", "Alex")] 
    public static long CalcScoreData()
    {
        return 0;
    }

    public static void AnotherMethod() { }

    [Custom("Jane", 1, "Method to display health data.")]
    public int DisplayScore(int score)
    {
        return score;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Type type = typeof(HealthScore);
        CustomAttribute classAttribute = (CustomAttribute)Attribute.GetCustomAttribute(type, typeof(CustomAttribute));
        PrintAttributeInfo(type, classAttribute);

        MethodInfo[] methods = type.GetMethods();
        foreach (MethodInfo method in methods)
        {
            CustomAttribute methodAttribute = (CustomAttribute)Attribute.GetCustomAttribute(method, typeof(CustomAttribute));
            PrintAttributeInfo(method, methodAttribute);
        }
    }

    static void PrintAttributeInfo(MemberInfo member, CustomAttribute attribute)
    {
        if (attribute != null)
        {
            Console.WriteLine($"Information about {member.MemberType}: {member.Name}");
            Console.WriteLine($"  Author: {attribute.Author}");
            Console.WriteLine($"  Revision: {attribute.Revision}");
            Console.WriteLine($"  Description: {attribute.Description}");
            Console.WriteLine($"  Reviewers: {string.Join(", ", attribute.Reviewers)}");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"Information about {member.MemberType}: {member.Name} - don't have informatino");
            Console.WriteLine();
        }
    }
}
