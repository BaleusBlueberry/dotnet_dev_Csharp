using System;
// See https://aka.ms/new-console-template for more information

namespace Ex2solution
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int result = GetValidNumbers("please enter the ammount of pairs you want:");

            int[,] numbers = new int[result, 2];

            
        }

        static int GetValidNumbers(string massage)
        {
            Console.WriteLine(massage);
            bool check = true;
            int input;

            while (check)
            {
                bool success = int.TryParse(Console.ReadLine(), out input);
                
                if (success)
                {
                    check = false;
                }
                else 
                {
                    Console.WriteLine("inputed number is not an integer");
                }
            }

            return input;
        }

        static double CalculateResult(int num1, int num2)
        {

        }
    }

}