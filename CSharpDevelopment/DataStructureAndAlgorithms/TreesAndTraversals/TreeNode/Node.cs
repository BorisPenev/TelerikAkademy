using System;
using System.Collections.Generic;

namespace TreeNode
{
    class Node<T> : IComparable<Node<T>>
    {
        public T Value { get; set; }
        public Node<T> Parent { get; set; }

        private SortedSet<Node<T>> childs = new SortedSet<Node<T>>();
        public SortedSet<Node<T>> Childs
        {
            get { return childs; }
            set { childs = value; }
        }

        public int CompareTo(Node<T> other)
        {
            return Comparer<T>.Default.Compare(other.Value, this.Value);
        }
    }
}
