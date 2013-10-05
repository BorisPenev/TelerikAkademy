using System;
using System.Linq;

namespace CustomException
{
    public class InvalidRangeException<T> : Exception
    {
        public T Start { get; set; }
        public T End { get; set; }

        public InvalidRangeException(string message, T start, T end)
            : base(message)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
