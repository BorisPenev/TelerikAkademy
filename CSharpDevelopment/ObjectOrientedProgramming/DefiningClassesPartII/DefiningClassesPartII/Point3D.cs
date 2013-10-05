using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiningClassesPartII
{
    public struct Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        
        private static readonly Point3D point0 = new Point3D(0, 0, 0);
        public static Point3D Point0
        {
            get { return Point3D.point0; }
        } 


        public Point3D(double x, double y, double z): this()
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
