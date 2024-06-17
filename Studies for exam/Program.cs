using System.Security.Cryptography.X509Certificates;

namespace Studies_for_exam;

internal class Program
{
    static void Main(string[] args)
    {
        // value type (it has the value) , or refrance type (that points to another location with a value)
        int val = 1; //values
        int val2 = val; // values

        object refrace1 = new object(); //its a refrence 

        object refrace2 = refrace1; //its a refrence 

        int[] arr = new int[2] { 10, 20 }; //its a refrence 

        int[] arr2 = arr;

        Console.WriteLine(arr[0]);

        int x = arr[0];
        arr[0] = 30;

        // x will now still 10
        // even if arr[0] will be




        // boxing ,
        // it creates an object and puts the val inside of it
        object b1 = val;

        object b2 = b1;

        b2 = 50;

        Console.WriteLine(b1);

        // b2 = 50   b1 = 1



        // unboxing 
        int v2 = (int)b1;



        int a; //will be 0
        string s; //will be 


        // if something returns a value its a fuction and if something dosen't return somethings its a method 
        Console.WriteLine(person.Name);
    }
}



public sealed class SingleType
{
    private static SingleType instance;


}


// vertual can be replaced by a override 

// await and async let the code contine

// abstract  

// obserable collection interface makes a thing

// xamel is what we use to write wpf

// x = 9 % 4 === 1 

// linq is used in iinurable  

// interface in a class

//wrappannel makes it flow and put it next to it

// stackpannel makes in into the bottom

// constructor is a function that creates the bace stats of the class

//inum is a opteions 

static class person
{
    private static string? _name;

    public static string Name { get => _name ??= "jhon"; set => _name = value; }
}

