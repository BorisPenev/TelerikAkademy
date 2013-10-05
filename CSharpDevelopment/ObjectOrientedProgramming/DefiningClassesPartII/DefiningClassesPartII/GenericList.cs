using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiningClassesPartII
{
    public class GenericList<T>
    {
        private static readonly int CAPACITY = 4;
        private int count = 0;
        private T[] elements = new T[CAPACITY];

        public GenericList(int capacity)
        {
            this.elements = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return elements[index];
            }
        }

        public void Add(T element)
        {
            if (count + 1 > this.elements.Length)
                ExtendElements();
            this.elements[count] = element;
            count++;
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            var tempElements = elements.Clone() as T[];
            elements = new T[tempElements.Length];
            for (int i = 0, j = 0; i < tempElements.Length; i++, j++)
            {
                if (i != index)
                {
                    elements[j] = tempElements[i];
                }
                else
                {
                    j--;
                }
            }
            count--;
        }

        public void InsertAt(int index, T element)
        {
            CheckIndex(index);
            if (count + 1 > this.elements.Length)
                ExtendElements();
            if (this.elements.Length < index)
            {
                this.elements[index] = element;
            }
            else
            {
                var tempElements = elements.Clone() as T[];
                elements = new T[tempElements.Length];
                for (int i = 0; i < index; i++)
                {
                    elements[i] = tempElements[i];
                }
                elements[index] = element;
                for (int i = index + 1, j = index; i < tempElements.Length; i++, j++)
                {
                    elements[i] = tempElements[j];
                }
            }
            count++;
        }

        public void Clear()
        {
            this.elements = new T[this.elements.Length]; 
            this.count = 0;
        }

        public int Find(T element)
        {
            return Array.IndexOf(this.elements, element);
        }

        public T Min<T>() where T : IComparable<T>, IComparable
        {
            return this.elements.Min() as dynamic;
        }

        public T Man<T>() where T : IComparable<T>, IComparable
        {
            return this.elements.Max() as dynamic;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, elements.Select(x => x.ToString()).ToArray());
        }

        private void CheckIndex(int index)
        {
            if (index > count || index < 0)
            {
                throw new IndexOutOfRangeException(String.Format("Invalid index: {0}.", index));
            }
        }

        private void ExtendElements()
        {
            T[] tempElements = new T[this.elements.Length * 2];
            Array.Copy(elements, 0, tempElements, 0, elements.Length);
            elements = tempElements;
        }
    }
}
