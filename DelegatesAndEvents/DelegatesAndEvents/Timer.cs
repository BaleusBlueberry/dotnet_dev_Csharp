using System;
namespace DelegatesAndEvents;

public delegate void TimerNotification(int time);

public class Timer
{
    public event TimerNotification TimerCompleted;
    public event TimerNotification TimerTick;


    public void Start(int time)
    {

        for (int i = 0; i < time; i++)
        {
            Thread.Sleep(1000);
            OnTimerTick(i);
        }

        OnTimerCompleted(time);
    }

    private void OnTimerCompleted(int time)
    {
        if (TimerCompleted == null)
        {
            return;
        }

        TimerCompleted(time+1);
    }
    private void OnTimerTick(int time)
    {
        if (TimerTick == null)
        {
            return;
        }

        TimerTick(time+1);
    }


}
