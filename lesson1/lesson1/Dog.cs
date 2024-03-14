using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson1
{
    internal class Dog
    {
        public string dogName;
        public string dogtype;
        private int barks = 0;

        public void Bark()
        {
            Console.WriteLine("the dog " + dogName + ": Bark Bark");
            barks++;
        }
        public void BarkAmmount()
        {
            Console.WriteLine(barks);
        }
    }

}

namespace lesson1dog2
{
    internal class Dog
    {
        public string dogName;
        public string dogtype;

        public void Bark()
        {
            Console.WriteLine("the dog is barking in the second way");
            
        }
    }
}
