using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiningClassesPartII
{
    public class Path
    {
        private List<Point3D> space = new List<Point3D>();

        public List<Point3D> Space
        {
            get 
            {
                if (this.Space.Count == 0)
                {
                    this.space.Add(new Point3D(1, 1, 1));
                    this.space.Add(new Point3D(10, 10, 10));
                    this.space.Add(new Point3D(15, 15, 15));
                }

                return space; 
            }
            set { space = value; }
        }

    }
}
