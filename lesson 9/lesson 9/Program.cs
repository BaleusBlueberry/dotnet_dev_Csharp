namespace lesson_9
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // build an interface called "ILogger"
            // add a method called "Log" that takes a string
            // build a class called "FileLogger" that implements ILogger and writes to a file
            // build a class called ConsoleLogger that implements ILogger and writes to the console

            //File.WriteAllText(@"c:\temp\log.txt", "THIS IS A LINE OF TEXT!");
            // DateTime.Now.ToString();

            // create a List of ILogger and add an instance of each class
            // in a loop call the "Log" method on each item and verify results

            /*            Console.WriteLine("write a spasified file path");
                        string filePath = Console.ReadLine();

                        ISpeak logger = new FileLogger(filePath);
                        Console.WriteLine("write a massage");
                        string massage = Console.ReadLine();
                        logger.Log(massage);
                        Console.ReadLine(); */
            List<ISpeak> loggers = new List<ISpeak>();
            loggers.Add(new FileLogger());
            loggers.Add(new ConsoleLogger());
            loggers.Add(new ConsoleLogger());
            loggers.Add(new ConsoleLogger());

            foreach (ISpeak logger in loggers)
            {
                logger.Log($"this is ameria now: {DateTime.Now}");
            }
        }
    }
}
