using System;

namespace Methods.Utils
{
    public static class Mathematical
    {
        public static int Max(params int[] elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements can't be null");
            }

            if (elements.Length == 0)
            {
                throw new ArgumentException("elements is empty");
            }

            for (int i = 1; i < elements.Length; i++)
            {
                if (elements[i] > elements[0])
                {
                    elements[0] = elements[i];
                }
            }
            return elements[0];
        }
    }
}
