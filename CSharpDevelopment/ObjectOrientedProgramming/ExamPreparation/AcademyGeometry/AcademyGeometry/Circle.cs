using GeometryAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyGeometry
{
    public class Circle : Figure, IAreaMeasurable, IFlat
    {
        private double radius;

        public Circle(Vector3D center, double radius)
            :base (center)
        {
            this.radius = radius;
        }
        public Vector3D GetNormal()
        {
            return new Vector3D(0, 0, 1);
        }

        public override double GetPrimaryMeasure()
        {
            return this.GetArea();
        }

        public double GetArea()
        {
            return Math.PI * this.radius * this.radius;
        }
    }
}
