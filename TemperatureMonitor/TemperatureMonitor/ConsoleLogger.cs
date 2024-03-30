namespace TemperatureMonitor;

internal class ConsoleLogger
{
    public ConsoleLogger(TemperatureMonitor monitor)
    {
        monitor.TemperatureChange += HandleTemperatureChange;
        monitor.HighTemperatureAlert += HandleHighTemperature;
        monitor.LowTemperatureAlert += new Action<int, string>(HandleLowTemperature);
    }

    private void HandleTemperatureChange(int temp)
    {
        Console.WriteLine($"the temperature now is: {temp}");
    }

    private void HandleHighTemperature(int temp, string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    private void HandleLowTemperature(int temp, string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}