using System;
using System.Collections.Generic;
using System.Linq;

namespace Midget
{
    public class Patterns
    {
        private List<int> pattern = new List<int>();
        public List<int> Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        private int sum = 0;
        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }
    }
}
