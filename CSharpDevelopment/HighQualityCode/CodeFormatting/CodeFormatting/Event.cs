using System;
using System.Text;

namespace CodeFormatting
{
    class Event : IComparable
    {
        private readonly DateTime date;
        private readonly String title;
        private readonly String location;

        public Event(DateTime date, String title, String location)
        {
            this.date = date;
            this.title = title;
            this.location = location;
        }

        public int CompareTo(object obj)
        {
            var other = obj as Event;

            if (other == null)
            {
                return 1;
            }

            var byDate = date.CompareTo(other.date);
            if (title != null && location != null)
            {
                var byTitle = title.CompareTo(other.title);
                var byLocation = location.CompareTo(other.location);

                if (byDate == 0)
                {
                    return byTitle == 0 ? byLocation : byTitle;
                }

                return byDate;
            }

            return -1;
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.Append(date.ToString("yyyy-MM-ddTHH:mm:ss"));
            toString.Append(" | " + title);

            if (!string.IsNullOrEmpty(location))
            {
                toString.Append(" | " + location);
            }
         
            return toString.ToString();
        }
    }
}