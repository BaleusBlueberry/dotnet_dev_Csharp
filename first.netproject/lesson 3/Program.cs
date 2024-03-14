using System;
// See https://aka.ms/new-console-template for more information

namespace helloworld
{
    class Hello
    {

        static void Main()
        {
            /*  int inputs;
              Console.WriteLine("write a number");
              int.TryParse(Console.ReadLine(), out inputs);
              Doubleint(ref inputs, 2);
              Console.WriteLine(inputs);

              static void Doubleint(ref int num1, int by)
              {
                  num1 = num1 * by;
              }
              */

            /* int result; 

             SqurIt(1, out result);
             Console.WriteLine(result); 
 */
            Console.WriteLine(SumNums(1, 56, 4, 7, 7, 4, 3));

        }
        static void SqurIt(int num1, out int result)
        {
                result = num1 * 24;
        }
        static int SumNums(params int[] numbs)
        {
            int sumnumbs = 0;
            foreach (int num in numbs) 
            { 
                sumnumbs += num;

            }
            return sumnumbs;
        }
    }
}