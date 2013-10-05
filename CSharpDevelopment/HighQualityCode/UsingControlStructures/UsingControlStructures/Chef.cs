namespace UsingControlStructures
{
    public class Chef
    {
        internal void Cook(Vegetable vegetable)
        {
            var bowl = GetBowl();
            bowl.Add(vegetable);
        }

        internal Potato GetPotato()
        {
            var potato = new Potato();

            Peel(potato);
            Cut(potato);

            return potato;
        }

        internal Carrot GetCarrot()
        {
            var carrot = new Carrot();
            
            Peel(carrot);
            Cut(carrot);

            return carrot;
        }

        internal Bowl GetBowl()
        {
            return new Bowl();
        }

        private void Cut(Vegetable vegetable)
        {
            //throw new NotImplementedException();
        }

        private void Peel(Vegetable vegetable)
        {
            vegetable.HasNotBeenPeeled = true;
        }
    }
}
