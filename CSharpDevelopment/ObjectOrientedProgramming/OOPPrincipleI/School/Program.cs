using System;
using System.Collections.Generic;
using System.Linq;

namespace School
{
    class Program
    {
        static void Main()
        {
            List<Discipline> discipline = new List<Discipline>();
            discipline.Add(new Discipline() { Name = "C# Development", NumberLectures = 4, NumberOfExercises = 4 });

            List<Teacher> teachers = new List<Teacher>();
            teachers.Add(new Teacher() { Name = "Ivan", Discipline = discipline });

            List<Student> students = new List<Student>();
            students.Add(new Student() { Name = "Pesho", Teachers = teachers });
        }
    }
}
