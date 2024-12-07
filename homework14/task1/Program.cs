using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        string russianString = "Привет, мир!";
        string germanString = "Hallo Welt!";
        string japaneseString = "こんにちは世界！";

        SerializeAndDeserialize(russianString, "Русский");
        SerializeAndDeserialize(germanString, "Немецкий");
        SerializeAndDeserialize(japaneseString, "Японский");
    }

    static void SerializeAndDeserialize(string inputString, string language)
    {
        Encoding encoding = Encoding.UTF8; 

        byte[] byteArray = encoding.GetBytes(inputString);
        Console.WriteLine($"\n--- {language} ---");
        Console.WriteLine($"Original string: {inputString}");
        Console.WriteLine($"Array of bytes (length: {byteArray.Length}):");
        PrintByteArray(byteArray);

        string deserializedString = encoding.GetString(byteArray);
        Console.WriteLine($"Deserialized string: {deserializedString}");
        Console.WriteLine($"Do the lines equal? {inputString == deserializedString}");
    }

    static void PrintByteArray(byte[] array)
    {
        foreach (byte b in array)
        {
            Console.Write($"{b:X2} ");
        }
        Console.WriteLine();
    }
}
