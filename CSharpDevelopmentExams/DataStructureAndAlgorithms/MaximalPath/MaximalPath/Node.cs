using System.Collections.Generic;

namespace MaximalPath
{
    class Node
    {
        public int Id { get; set; }
        public HashSet<Node> Childrens { get; set; }

        public Node(int id)
        {
            this.Id = id;
            this.Childrens = new HashSet<Node>();
        }
    }
}
