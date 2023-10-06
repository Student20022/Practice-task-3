using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the Person class
        Person person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30
        };

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
        using (TextWriter writer = new StreamWriter("person.xml"))
        {
            xmlSerializer.Serialize(writer, person);
        }
        Console.WriteLine("Serialized using XML format.");

        using (TextReader reader = new StreamReader("person.xml"))
        {
            Person deserializedPerson = (Person)xmlSerializer.Deserialize(reader);
            Console.WriteLine($"Deserialized Person: {deserializedPerson.FirstName} {deserializedPerson.LastName}, Age: {deserializedPerson.Age}");
        }

        IFormatter binaryFormatter = new BinaryFormatter();
        using (Stream stream = new FileStream("person.bin", FileMode.Create, FileAccess.Write))
        {
            binaryFormatter.Serialize(stream, person);
        }
        Console.WriteLine("Serialized using binary format.");

        using (Stream stream = new FileStream("person.bin", FileMode.Open, FileAccess.Read))
        {
            Person deserializedPerson = (Person)binaryFormatter.Deserialize(stream);
            Console.WriteLine($"Deserialized Person: {deserializedPerson.FirstName} {deserializedPerson.LastName}, Age: {deserializedPerson.Age}");
        }
    }
}
