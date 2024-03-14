
using System.Runtime.CompilerServices;

namespace lesson1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Employ employ = new Employ();
            employ.firstName = "Jonny";
            employ.lastName = "katish";
            employ.DoWork();

           

            Dog pincher = new Dog();
            Dog lavrador = new Dog();
            Dog yimpor = new Dog();

            pincher.dogName = "yami";
            lavrador.dogName = "rexi";
            yimpor.dogName = "chif chif";

            pincher.Bark();
            lavrador.Bark();
            yimpor.Bark();
            yimpor.Bark();
            yimpor.Bark();
            yimpor.Bark();
            yimpor.BarkAmmount();

            pincher.dogtype = "pincher";

            lesson1dog2.Dog dogNewNamespace = new lesson1dog2.Dog();
            dogNewNamespace.Bark();





        }
    }
}