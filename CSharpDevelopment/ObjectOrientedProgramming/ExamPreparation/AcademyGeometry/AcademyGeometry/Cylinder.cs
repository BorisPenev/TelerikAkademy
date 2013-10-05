using GeometryAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyGeometry
{
    public class Cylinder : Figure, IAreaMeasurable, IVolumeMeasurable
    {
        private Vector3D bottomCircle;
        private Vector3D topCircle;
        private double radius;

        public Cylinder(Vector3D bottomCircle, Vector3D topCircle, double radius)
            :base(bottomCircle, topCircle)
        {
            // TODO: Complete member initialization
            this.bottomCircle = bottomCircle;
            this.topCircle = topCircle;
            this.radius = radius;
        }
        public double GetArea()
        {
            double topAndBottom = (Math.PI * this.radius * this.radius) * 2;
            double side = 2 * Math.PI * this.radius * (this.topCircle - this.bottomCircle).Magnitude;
            return topAndBottom + side;
        }

        public double GetVolume()
        {
            return Math.PI * this.radius * this.radius * (this.topCircle - this.bottomCircle).Magnitude;
        }

        public override double GetPrimaryMeasure()
        {
            return this.GetVolume();
        }
    }
}
