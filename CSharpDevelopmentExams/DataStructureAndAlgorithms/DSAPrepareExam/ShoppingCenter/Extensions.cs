namespace ShoppingCenter
{
    public static class Extensions
    {
        public static bool ContainsAny(this string source, char[] compare)
        {
            return source.IndexOfAny(compare) >= 0;
        }
    }
}
