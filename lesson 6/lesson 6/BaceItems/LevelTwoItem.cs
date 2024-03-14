using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_6.BaceItems
{
    internal class LevelTwoItem : LevelOneItem
    {
        public override void ExecuteCommanAction()
        {
            Console.WriteLine("leveled up, level 2 ");
        }
    }
}
