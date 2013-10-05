using System.Collections.Generic;

namespace MinimizeTheCostForCables
{
    class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Connection> Connections { get; set; }
 
        public House(string name)
        {
            this.Id = int.MaxValue;
            this.Name = name;
            this.Connections = new List<Connection>();
        }
    }
}
