using System;
namespace DelegatesAndEvents;

public delegate void TimerNotification(int time, string massage);
public delegate void TimerCompleate(string massage);

public class Timer
{
    public event TimerCompleate TimerCompleted;
    public event TimerNotification TimerTick;


    public void Start(int time)
    {

        for (int i = 0; i < time; i++)
        {
            Thread.Sleep(500);
            OnTimerTick(i, "the time that has passed: ");
        }

        OnTimerCompleted("the timer has finished");
    }

    private void OnTimerCompleted(string massage)
    {
        if (TimerCompleted == null)
        {
            return;
        }

        TimerCompleted(massage);
    }

    private void OnTimerTick(int time, string massage)
    {
        if (TimerTick == null)
        {
            return;
        }

        TimerTick(time, massage);
    }
}
