using System;
using System.Collections.Generic;
using System.Linq;

namespace FallingRocks
{
    public class Rock
    {
        private static readonly List<string> rockTypes = new List<string>();
        private static readonly List<ConsoleColor> rockColors = new List<ConsoleColor>();

        private string type = string.Empty;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private ConsoleColor color = ConsoleColor.White;
        public ConsoleColor Color
        {
            get { return color; }
            set { color = value; }
        }

        private sbyte size = 1;
        public sbyte Size
        {
            get { return size; }
            set { size = value; }
        }

        private sbyte x = 0;
        public sbyte X
        {
            get { return x; }
            set { x = value; }
        }

        private sbyte y = 0;
        public sbyte Y
        {
            get { return y; }
            set { y = value; }
        }

        private bool isActive = false;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public Rock()
        {
            if (rockTypes.Count == 0)
            {
                rockTypes.Add("~");
                rockTypes.Add("!");
                rockTypes.Add("@");
                rockTypes.Add("#");
                rockTypes.Add("$");
                rockTypes.Add("%");
                rockTypes.Add("^");
                rockTypes.Add("&");
                rockTypes.Add("*");
                rockTypes.Add(";");
                rockTypes.Add(".");
                rockTypes.Add("-");
            }

            if (rockColors.Count == 0)
            {
                rockColors.Add(ConsoleColor.White);
                rockColors.Add(ConsoleColor.Green);
                rockColors.Add(ConsoleColor.Red);
                rockColors.Add(ConsoleColor.Blue);
                rockColors.Add(ConsoleColor.Yellow);
            }

            this.Type = rockTypes[Helpers.RandomNumber(0, rockTypes.Count - 1)];
            this.Color = rockColors[Helpers.RandomNumber(0, rockColors.Count - 1)];
            this.Size = Helpers.RandomNumber(1, 4);
        }
    }
}
