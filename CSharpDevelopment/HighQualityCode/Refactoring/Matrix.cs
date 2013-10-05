using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkInMatrix
{
    public class Matrix
    {
        private int[,] gameMatrix;
        public int[,] GameMatrix
        {
            get { return gameMatrix; }
            internal set { gameMatrix = value; }
        }

        /// <summary>
        /// Set maximum direction count.
        /// </summary>
        private int directionLeftCount = Enum.GetNames(typeof(DirectionTypes)).Length;
        public int DirectionLeftCount
        {
            get { return directionLeftCount; }
            internal set { directionLeftCount = value; }
        }

        private DirectionTypes directionType = DirectionTypes.Southeast;
        public DirectionTypes DirectionType
        {
            get { return directionType; }
            set { directionType = value; }
        }

        private Field direction = new Field(1, 1);
        internal Field Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private Field currentPosition = new Field(0, 0);
        internal Field CurrentPosition
        {
            get { return currentPosition; }
            set { currentPosition = value; }
        }

        public int MaxValue { get; set; }

        public Matrix(int[,] gameMatrix)
        {
            this.GameMatrix = gameMatrix;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < this.GameMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < this.GameMatrix.GetLength(0); col++)
                {
                    sb.AppendFormat("{0,3}", this.GameMatrix[row, col]);
                }

                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
