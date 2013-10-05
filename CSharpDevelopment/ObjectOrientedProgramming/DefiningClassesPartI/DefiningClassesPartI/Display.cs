using System;
using System.Linq;

namespace DefiningClassesPartI
{
    public class Display
    {
        private double size = 0;
        public double Size
        {
            get { return size; }
            set { size = value; }
        }

        private ColorsTypes numberOfColors = ColorsTypes.Normal;
        public ColorsTypes NumberOfColors
        {
            get { return numberOfColors; }
            set { numberOfColors = value; }
        }

    }
}
