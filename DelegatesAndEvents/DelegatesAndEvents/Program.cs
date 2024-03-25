using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace DelegatesAndEvents
{
    internal class Program
    {
        public delegate void MyDelegate(string massage);

        static void Main(string[] args)
        {
            /*MyDelegate del = new MyDelegate(MyFunction);

            del("www u www");*/

            // make a list of functions

            MyBusnessLogic bl = new MyBusnessLogic();

            bl.FinishedWorking += new Notify(MyFunction);
            bl.DoingWork += new Update(HandleDoingWork);
            bl.StartWorking();

            Console.ReadLine();
            return;

            List<MenuAction> methods = new List<MenuAction>
            {
                ShowGreeting, DisplayDate, DisplayTime,
            };

            int userChoice;


            while (true)
            {
                Console.WriteLine("Select a number between 1 and 3:");
                if (int.TryParse(Console.ReadLine(), out userChoice) && userChoice >= 1 && userChoice <= 3)
                {
                    int input = userChoice - 1;

                    methods[input]();

                } else {
                     Console.ForegroundColor = ConsoleColor.Yellow;
                     Console.WriteLine("Invalid input. Please enter a valid number between 1 and 3.");
                     Console.ResetColor();

                }
            }
        }

        private static void Bl_DoingWork(int precentComplete, string massage)
        {
            throw new NotImplementedException();
        }

        public static void MyFunction(string massage)
        {
            Console.WriteLine(massage);
        }

        public static void MyFunction2(string text)
        {
            File.AppendAllText("log.txt", text + Environment.NewLine);
        }

        public delegate void MenuAction();

        public static void ShowGreeting()
        {
            Console.WriteLine("Greetings Traveler");
        }
        public static void DisplayDate()
        {
            Console.WriteLine("the date is: " + DateTime.Now.ToShortDateString());
        }
        public static void DisplayTime()
        {
            Console.WriteLine("the time is:" + DateTime.Now.ToShortTimeString());
        }

        public static void HandleDoingWork(int precentComplete, string massage)
        {
            Console.WriteLine($"finished working: {precentComplete}% ");
            Console.WriteLine(massage);
        }
    }


}
