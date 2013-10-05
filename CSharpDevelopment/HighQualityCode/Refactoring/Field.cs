using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkInMatrix
{
    class Field
    {
        private int row = 0;
        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        private int col = 0;
        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        public Field(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
    }
}
