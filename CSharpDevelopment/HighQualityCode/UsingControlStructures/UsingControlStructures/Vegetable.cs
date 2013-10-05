namespace UsingControlStructures
{
    public abstract class Vegetable
    {
        private bool hasNotBeenPeeled = false;
        public bool HasNotBeenPeeled
        {
            get { return hasNotBeenPeeled; }
            set { hasNotBeenPeeled = value; }
        }

        private bool isRotten = false;
        public bool IsRotten
        {
            get { return isRotten; }
            set { isRotten = value; }
        }
    }
}
