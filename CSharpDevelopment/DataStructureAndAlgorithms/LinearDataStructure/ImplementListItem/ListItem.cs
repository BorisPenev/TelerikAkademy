namespace ImplementListItem
{
    class ListItem<T>
    {
        public T value { get; set; }
        public ListItem<T> NextItem { get; set; }
    }
}
