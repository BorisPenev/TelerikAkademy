using System.Collections.Generic;
using System.Linq;

namespace ImplementQueue
{
    class LinkedQueue<T>
    {
        LinkedList<T> elements = new LinkedList<T>();
 
        public void Enqueue(T value)
        {
            elements.AddLast(value);
        }

        public T Dequeue()
        {
            var el = elements.First();
            elements.RemoveFirst();
            return el;
        }

        public T Peek()
        {
            return elements.First();
        }
    }
}
