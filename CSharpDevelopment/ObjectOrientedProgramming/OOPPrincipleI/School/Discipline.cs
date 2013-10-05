using System;
using System.Linq;

namespace School
{
    class Discipline : CommentService
    {
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int numberLectures = 0;
        public int NumberLectures
        {
            get { return numberLectures; }
            set { numberLectures = value; }
        }

        private int numberOfExercises = 0;
        public int NumberOfExercises
        {
            get { return numberOfExercises; }
            set { numberOfExercises = value; }
        }
    }
}
