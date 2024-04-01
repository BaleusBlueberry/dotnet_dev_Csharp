namespace AccessModifires2;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        Animal catis = new Cat();

        catis.MakeSound();

        Console.ReadLine();
    }
}
