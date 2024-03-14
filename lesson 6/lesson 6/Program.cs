using lesson_6.BaceItems;
using lesson_6.ClassProject;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace lesson_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("how many items do you want");
            if (!int.TryParse(Console.ReadLine(), out int arrayLength)) {
                Console.WriteLine("invalid input, using defuly 10");
                arrayLength = 10;
            }

            BaceItem[] items = new BaceItem[arrayLength];

            int mainLoop = 0;

            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"what level do you want your item to be in possition {i}");

                string userChoise = Console.ReadLine();

                switch (userChoise)
                {
                    case "1":
                        items[i] = new LevelOneItem(); break;
                    case "2":
                        items[i] = new LevelTwoItem(); break;
                    case "3":
                        items[i] = new LevelTreeItem(); break;
                }

            }
            Console.WriteLine("the results are :");
            foreach (BaceItem item in items)
            {
                item.ExecuteCommanAction();
            }


        }
    }
}
