using System.Diagnostics;

class ImmutablePerson
{
  private readonly string _firstName;
  private readonly string _lastName;
  private readonly int _age;

  public ImmutablePerson(string firstName, string lastName, int age)
  {
    _firstName = firstName;
    _lastName = lastName;
    _age = age;
  }

  public string FirstName => _firstName;
  public string LastName => _lastName;
  public int Age => _age;

  public ImmutablePerson WithFirstName(string newFirstName)
  {
    return new ImmutablePerson(newFirstName, _lastName, _age);
  }

  public ImmutablePerson WithLastName(string newLastName)
  {
    return new ImmutablePerson(_firstName, newLastName, _age);
  }

  public ImmutablePerson WithAge(int newAge)
  {
    return new ImmutablePerson(_firstName, _lastName, newAge);
  }
}

class Program
{
    static void Main(string[] args)
    {
        ImmutablePerson person = new ImmutablePerson("John", "Doe", 30);
        ImmutablePerson personWithUpdatedFirstName = person.WithFirstName("Jane");
        ImmutablePerson personWithUpdatedLastName = person.WithLastName("Cena");
        ImmutablePerson personWithUpdatedAge = person.WithAge(60);


        Debug.Assert(
            person.FirstName == "John" &&
            person.LastName == "Doe" && 
            person.Age == 30
        );
        Debug.Assert(
            personWithUpdatedFirstName.FirstName == "Jane" &&
            personWithUpdatedFirstName.LastName == "Doe" && 
            personWithUpdatedFirstName.Age == 30
        );
        Debug.Assert(
            personWithUpdatedLastName.FirstName == "John" &&
            personWithUpdatedLastName.LastName == "Cena" && 
            personWithUpdatedLastName.Age == 30
        );
        Debug.Assert(
            personWithUpdatedAge.FirstName == "John" &&
            personWithUpdatedAge.LastName == "Doe" && 
            personWithUpdatedAge.Age == 60
        );

        Console.WriteLine("Success");
    }
}
