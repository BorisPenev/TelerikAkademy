namespace ImplementStack
{
    public class Stack<T>
    {
        private T[] elements = new T[8];
        private int activeIndex = 0;

        public void Push(T value)
        {
            if (activeIndex >= elements.Length)
            {
                var temElements = new T[activeIndex * 2];
                for (int i = 0; i < elements.Length; i++)
                {
                    temElements[i] = elements[i];
                }
                elements = temElements;
            }
            elements[activeIndex] = value;
            activeIndex++;
        }

        public T Pop()
        {
            activeIndex--;
            var el = elements[activeIndex];
            elements[activeIndex] = default(T);
            return el;
        }
    }
}
