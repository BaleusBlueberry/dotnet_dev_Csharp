using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_2
{
    internal class Cat
    {

        public string name;
        public int age;
        public string color;
        public int livesLeft;

        public string Name
        {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }
        public int LivesLeft
        {
            get
            {
                return livesLeft;
            }
            set
            {
                livesLeft = value;
            }
        }
        public void PrintStats()
        {
            Console.WriteLine($"Cat name: {name}");
            Console.WriteLine($"Cat colour: {color}");
            Console.WriteLine($"Cat age: {age}"); Console.WriteLine($"Cat name: {name}");
            Console.WriteLine($"Cat lives: {livesLeft}");

        }

    }
}
