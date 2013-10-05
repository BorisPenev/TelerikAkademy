namespace NamingIdentifier
{
    partial class Mines
    {
        public class Champion
        {
            readonly string name;
            readonly int points;

            public string Name
            {
                get
                {
                    return name;
                }
            }

            public int Points
            {
                get
                {
                    return points;
                }
            }

            public Champion(string name, int points)
            {
                this.name = name;
                this.points = points;
            }
        }
    }
}