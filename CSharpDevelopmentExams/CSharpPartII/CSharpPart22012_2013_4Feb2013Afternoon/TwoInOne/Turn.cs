using System;
using System.Linq;

namespace TwoInOne
{
    public class Turn
    {
        private int number = 0;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private bool isCaptured = false;

        public bool IsCaptured
        {
            get { return isCaptured; }
            set { isCaptured = value; }
        }
    }
}
