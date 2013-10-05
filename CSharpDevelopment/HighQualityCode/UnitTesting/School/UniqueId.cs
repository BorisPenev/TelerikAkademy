using System;
using System.Collections.Generic;

namespace School
{
    public static class UniqueId
    {
        public const int MinId = 10000;
        public const int MaxId = 99999;
        internal static int currentId = MinId;

        public static int NewId()
        {
            if (currentId < MaxId)
            {
                return currentId++;
            }
            throw new ArgumentOutOfRangeException("Max ID found. Please use Shrink() method to regenerate id's");
        }

        public static bool Shrink(IEnumerable<Student> source)
        {
            var count = MinId;
            foreach (var student in source)
            {
                count++;
                if (student.Id != count)
                {
                    student.Id = count;
                }
            }

            currentId = count;
            if (currentId >= MaxId)
            {
                throw  new ArgumentOutOfRangeException("source Array is full. Cannot regenerate id's");
            }

            return true;
        }
    }
}
