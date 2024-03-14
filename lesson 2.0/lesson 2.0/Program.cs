namespace lesson_2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<LibraryItem> 


            Book forestOfLove = new Book();
            forestOfLove.Title = "love";

        }
    }
    abstract class LibraryItem
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int YearPublished { get; set; }

        public abstract void DisplayInformation();

    }
    class Book : LibraryItem , ICheckable
    {
        public override void DisplayInformation()
        {
            Console.WriteLine($"ID: {ID} | Title: {Title} | YearPublished: {YearPublished} | Author: {Author} | Genre: {Genre}");
        }

        public string Author { get; set; }

        public string Genre { get; set; }

        public void CheckOut()
        {
            Console.WriteLine($"{Title} has checked out");
        }

        public void CheckIn()
        {
            Console.WriteLine($"{Title} has checked in");
        }

    }

    class Magazine : LibraryItem , ICheckable
    {
        public int IssueNumber { get; set; }

        public string Category { get; set; }

        public void CheckOut()
        {
            Console.WriteLine($"{Title} has checked out");
        }

        public void CheckIn()
        {
            Console.WriteLine($"{Title} has checked in");
        }

        public override void DisplayInformation()
        {
            Console.WriteLine($"ID: {ID} | Title: {Title} | YearPublished: {YearPublished} | IssueNumber: {IssueNumber} | Category: {Category}");
        }
    }

    interface ICheckable
    {
        void CheckIn();

        void CheckOut();
    }
}
