﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Myproject.MyLibrary
{
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
}
