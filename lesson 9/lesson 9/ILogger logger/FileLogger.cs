public interface ISpeak
{
    void Log(string message);
}

class FileLogger : ISpeak
{
    public void Log(string message)
    {
        File.WriteAllText("loggggg.txt", message);
    }
}

class ConsoleLogger : ISpeak
{
    public void Log (string message) {
    
        Console.WriteLine(message);
    }
}