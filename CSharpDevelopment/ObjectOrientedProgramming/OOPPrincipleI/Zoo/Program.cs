using System;
using System.Collections.Generic;
using System.Linq;

namespace Zoo
{
    class Program
    {
        static void Main()
        {
            List<Animal> animals = new List<Animal>();
            animals.Add(new Dog(1, "Caprika", SexType.Female));
            animals.Add(new Cat(7, "Lucy", SexType.Male));
            animals.Add(new Frog(2, "Figaro", SexType.Male));
            animals.Add(new Tomcat(5, "Tom"));
            animals.Add(new Kitten(4, "Maca"));

            Console.WriteLine(animals.Average(a => a.Age));
        }
    }
}
