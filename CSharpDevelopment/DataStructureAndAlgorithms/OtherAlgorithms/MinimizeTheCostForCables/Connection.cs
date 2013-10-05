using System;

namespace MinimizeTheCostForCables
{
    class Connection: IComparable<Connection>
    {
        public House House1 { get; set; }
        public House House2 { get; set; }
        public int Distance { get; set; }
        public bool IsMinPath { get; set; }

        public Connection()
        {
            this.IsMinPath = true;
        }

        public int CompareTo(Connection other)
        {
            int result = 0;

            result = this.Distance.CompareTo(other.Distance);
            if (result != 0)
            {
                return result;
            }

            result = -this.House1.Name.CompareTo(other.House1.Name);
            if (result != 0)
            {
                return result;
            }

            result = -this.House2.Name.CompareTo(other.House2.Name);
            if (result != 0)
            {
                return result;
            }

            return result;
        }
    }
}
