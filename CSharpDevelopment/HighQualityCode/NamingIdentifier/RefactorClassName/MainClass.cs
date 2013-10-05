namespace RefactorClassName
{
    internal class MainClass
    {
        private enum Sex
        {
            Male,
            Girl
        };

        private class Human
        {
            public Sex Sex { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public void Make_Чуек(int id)
        {
            var human = new Human();
            human.Age = id;
            if (id % 2 == 0)
            {
                human.Name = "Батката";
                human.Sex = Sex.Male;
            }
            else
            {
                human.Name = "Мацето";
                human.Sex = Sex.Girl;
            }
        }

    }
}
