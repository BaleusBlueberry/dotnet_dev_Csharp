namespace lesson_2._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DerivedClass DC1 = new DerivedClass();

            if (DC1 is DerivedClass)
            {
                BaceClass Bace1 = DC1 as BaceClass;
            }
        }
    }
}
