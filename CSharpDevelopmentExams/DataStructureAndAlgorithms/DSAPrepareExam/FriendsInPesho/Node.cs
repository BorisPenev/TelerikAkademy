using System;
using System.Collections.Generic;

namespace FriendsInPesho
{
    class Node : IComparable<Node>
    {
        public int Id { get; set; }
        public int Distance { get; set; }
        public bool IsHospital { get; set; }
        public List<Connection> Connections { get; set; }

        public Node(int id)
        {
            this.Id = id;
            this.IsHospital = false;
            this.Connections = new List<Connection>();
        }

        public int CompareTo(Node other)
        {
            return this.Distance.CompareTo(other.Distance);
        }
    }
}
