namespace lesson_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserProfile user1 = new UserProfile();
            user1.DisplayProfile();

            Cat garfild = new Cat();

            garfild.Name = "garfild";
            garfild.LivesLeft = 5;
            garfild.Age = 10;
            garfild.color = "ginger";
            garfild.PrintStats();
        }
    }
}
