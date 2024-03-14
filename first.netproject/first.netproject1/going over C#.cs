// See https://aka.ms/new-console-template for more information

namespace helloworld
{
    class hello
    {
        static void main()
        {
            double num1 = 0;
            double num2 = 0;

            Console.WriteLine("please write the first number:"); 
            while (!double.TryParse(Console.ReadLine(), out num1)) {
                Console.WriteLine("invalid input please input a valid number");
            }

            Console.WriteLine("please write the second number:");
            while (!double.TryParse(Console.ReadLine(), out num2)) {
                Console.WriteLine("invalid input please input a valid number");
            }

            Console.WriteLine("Please select the operator to use: (+,-,/,*)");
            bool validOperation = false;
            double result = 0;
            string usertInput = ""; 
            while (!validOperation)

            {
                if (usertInput == "+") {
                    result = num1 + num2;
                    validOperation = true;
                } else if (usertInput == "-") {
                    result = num1 - num2;
                    validOperation = true;
                } else if (usertInput == "/") {
                    result = num1 / num2;
                    validOperation = true;
                } else if (usertInput == "*") {
                    result = num1 * num2;
                    validOperation = true;
                } else {
                    Console.WriteLine("Invalid opperation, please input a valid opperation");
                }

            }
            Console.WriteLine("The solution is: " + num1 + usertInput + num2 + " = " + result);
        }
    }
}


