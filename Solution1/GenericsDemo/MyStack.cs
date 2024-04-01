using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsDemo
{
    public class MyStack<T>
    {
        public readonly T[] _items;
        private int _currentIndex = -1;

        public MyStack() => _items = new T[10];

        public int Count => _currentIndex + 1;

        public void Push(T item)
        {
            _items[++_currentIndex] = item;
        }

        public T Pop()
        {
            return _items[_currentIndex--];
        }

        /*
        public double Pop() {
            return _items[_currentIndex--];
        }
        */

    }
}

