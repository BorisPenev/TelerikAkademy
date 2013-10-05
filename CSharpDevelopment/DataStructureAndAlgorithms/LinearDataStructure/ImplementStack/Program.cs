namespace ImplementStack
{
    class Program
    {
        static void Main()
        {
            var stack = new Stack<int>();
            stack.Push(5);
            stack.Push(11);
            stack.Push(22);
            stack.Push(33);
            stack.Push(44);
            stack.Pop();
            stack.Push(55);
            stack.Push(11);
            stack.Push(22);
            stack.Push(33);
            stack.Push(44);
            stack.Pop();
            stack.Push(55);
            stack.Push(11);
            stack.Push(22);
            stack.Push(33);
            stack.Push(44);
            stack.Pop();
            stack.Push(55);
            stack.Push(11);
            stack.Push(22);
            stack.Push(33);
            stack.Push(44);
            stack.Pop();
        }
    }
}
