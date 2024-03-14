using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_6.ClassProject
{
    internal class Car : Vehicle
    {
        public override void StartEngin()
        {
            Console.WriteLine("ovrided start engin");
        }
    }
}
