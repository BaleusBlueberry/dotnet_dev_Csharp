namespace DelegatesAndEvents;

public delegate void Notify(string message);
public delegate void Update(int precentComplete, string massage);

internal class MyBusnessLogic
{
    public event Notify FinishedWorking;
    public event Update DoingWork;


    private void OnFinishedWorking(string message)
    {
        if (FinishedWorking == null)
        {
            return;
        }
        FinishedWorking(message);
    }

    public void StartWorking()
    {
        Console.WriteLine("STARTED TO WORK....");
        for (int i = 0; i < 20; i++)
        {
            OnDoingWork(i * 5, "working...");
            Thread.Sleep(500);
        }

        OnFinishedWorking("Finished working boss!");
    }

    private void OnDoingWork(int percent, string text)
    {
        if (DoingWork == null)
        {
            return;
        }

        DoingWork(percent, text);
    }
}