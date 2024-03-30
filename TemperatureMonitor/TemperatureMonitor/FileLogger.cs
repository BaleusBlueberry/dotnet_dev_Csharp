namespace TemperatureMonitor;
internal class FileLogger
{
    public FileLogger(TemperatureMonitor monitor)
    {
        monitor.TemperatureChange += HandleTemperatureChange;
    }

    private void HandleTemperatureChange(int temp)
    {
        File.AppendAllText("temp.txt", $"the temperature right now is: {temp}" + Environment.NewLine);
    }
} 