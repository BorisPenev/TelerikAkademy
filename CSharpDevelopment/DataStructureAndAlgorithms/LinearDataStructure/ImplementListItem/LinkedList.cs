namespace ImplementListItem
{
    class LinkedList<T>
    {
        public ListItem<T> FirstElement { get; set; }
        private ListItem<T> CurrentElement { get; set; }

        public void Add(T value)
        {
            if (FirstElement == null)
            {
                FirstElement = new ListItem<T>();
                FirstElement.value = value;
            }
            else
            {
                if (CurrentElement == null)
                {
                    CurrentElement = new ListItem<T>();
                    CurrentElement.value = value;
                    FirstElement.NextItem = CurrentElement;
                }
                else
                {
                    var newElement = new ListItem<T>();
                    newElement.value = value;
                    CurrentElement.NextItem = newElement;
                    CurrentElement = newElement;
                }
            }
        }
    }
}
