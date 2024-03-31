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

public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("generic animal sound ");



    }

    protected void MakeAnotherSound()
    {
        Console.WriteLine("another sound");
    }
}

public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("mewing sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("mewing sound");
    }
}