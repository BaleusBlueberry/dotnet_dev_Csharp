using System;
using System.Threading;

namespace TemperatureMonitor;

public class TemperatureMonitor
{
    public event Action<int> TemperatureChange;
    public event Action<int, string> LowTemperatureAlert;
    public event Action<int, string> HighTemperatureAlert;

    public void Start()

    {
        var rnd = new Random();
        while (true)
        {
            Thread.Sleep(200);

            int currentTemperature = rnd.Next(-20, 50);

            OnTemperatureChange(currentTemperature);

            /*if (currentTemperature <= 0)
            {
                OnLowTemperatureAlert(currentTemperature);
            }
            else if (currentTemperature > 40)
            {
                OnHighTemperatureAlert(currentTemperature);
            }*/
        }
    }

    private void OnTemperatureChange(int temperature)
    {
        if (TemperatureChange != null)
        {
            TemperatureChange.Invoke(temperature);
        }

        OnAlert(temperature);
    }

    private void OnAlert(int temp) {

        if (temp > 40 && HighTemperatureAlert != null)
        {
            HighTemperatureAlert.Invoke(temp, $"Look out the temperature is: {temp}");

        } else if (temp < 0 && LowTemperatureAlert != null)
        {

            LowTemperatureAlert.Invoke(temp, $"Look out the temperature is: {temp}");
        }

    }


    /*
    private void OnLowTemperatureAlert(int temperature)
    {
        LowTemperatureAlert?.Invoke(temperature, $"Look out the temperature is: {temperature}");
    }

    private void OnHighTemperatureAlert(int temperature)
    {

        HighTemperatureAlert?.Invoke(temperature, $"Look out the temperature is: {temperature}");
    }

    */
}