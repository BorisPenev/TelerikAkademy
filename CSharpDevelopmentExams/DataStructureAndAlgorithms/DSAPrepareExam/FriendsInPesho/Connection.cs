namespace FriendsInPesho
{
    internal struct Connection
    {
        public Node ToNode { get; set; }
        public int Distance { get; set; }

        public Connection(Node toNode, int distance)
            : this()
        {
            this.ToNode = toNode;
            this.Distance = distance;
        }
    }
}
