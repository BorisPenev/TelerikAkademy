namespace Methods.Utils
{
    public static class Extensions
    {
        public static bool IsOlder(this Student source, Student other)
        {
            return source.BirthDate > other.BirthDate;
        }
    }
}
