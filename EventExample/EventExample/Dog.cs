using System.IO;
using System.Text.Json;

namespace EventExample;

public class Dog
{
    public string Name { get; set; }

    public string Breed { get; set; }

    public bool IsOwned { get; set; }

    public static List<Dog> GetListFromFile()
    {
        string rawData = File.ReadAllText("dogs.json");

        List<Dog> resultingList = JsonSerializer.Deserialize<List<Dog>>(rawData);

        return resultingList;
    }
}