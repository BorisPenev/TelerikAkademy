using System;

namespace UsingControlStructures
{
    class Program
    {
        private const int MIN_X = int.MinValue;
        private const int MAX_X = int.MaxValue;
        private const int MIN_Y = int.MinValue;
        private const int MAX_Y = int.MaxValue;

        private static bool shouldNotVisitCell = false;
        private static int x = 0;
        private static int y = 0;

        static void Main(string[] args)
        {
            bool isXInRange = MIN_X <= x && x <= MAX_X;
            bool isYInRange = MIN_Y <= y && y <= MAX_Y;

            if (isXInRange && isYInRange && !shouldNotVisitCell)
            {
                VisitCell();
            }
        }


        private static void VisitCell()
        {
            //throw new NotImplementedException();
        }

        public void Cook()
        {
            Chef chef = new Chef();
            var potato = chef.GetPotato();
            if (potato != null)
            {
                if (!potato.HasNotBeenPeeled && !potato.IsRotten)
                {
                    chef.Cook(potato);
                }
            }
        }

        public void RefactorLoop(int[] array, int expectedValue)
        {
            bool isValueFound = false;
            int i;

            for (i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);

                if (i % 10 == 0)
                {
                    if (array[i] == expectedValue)
                    {
                        isValueFound = true;
                        break;
                    }
                }
            }

            if (isValueFound)
            {
                Console.WriteLine("Value Found");
            }
        }
    }
}
