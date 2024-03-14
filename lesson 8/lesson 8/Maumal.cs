using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_8
{
    internal class Maumal : Animal
    {
        public bool IsDomestic { get; set; }

    }
    internal class Bird : Animal {
        public bool IsDomestic { get; set; }
    }

    interface ISpeak
    {
        void Talk();

        int Name { get; set; }
    }

    internal class Dog : ISpeak
    {
        public int Name { get; set; }

        public void Talk()
        {
            Console.WriteLine("Bark");
        }
    }
}
