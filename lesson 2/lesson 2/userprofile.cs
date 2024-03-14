using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_2
{
    internal class UserProfile
    {
        public readonly int userID;
        public readonly DateTime creationDate;


        public UserProfile()
        {
            Random rnd = new Random();
            userID = rnd.Next(int.MaxValue);

            creationDate = DateTime.Now;

        }
        public void DisplayProfile()
        {
            Console.WriteLine($"user: {userID}");
            Console.WriteLine($"creationDate: {creationDate}");
        }
    }
}
