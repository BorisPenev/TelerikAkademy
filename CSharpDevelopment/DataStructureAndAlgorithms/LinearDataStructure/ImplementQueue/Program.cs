namespace ImplementQueue
{
    class Program
    {
        static void Main()
        {
            var lq = new LinkedQueue<int>();
            lq.Enqueue(5);
            lq.Enqueue(11);
            lq.Enqueue(22);
            lq.Enqueue(33);
            lq.Enqueue(44);
            lq.Dequeue();
            var p = lq.Peek();
            lq.Enqueue(55);
            lq.Enqueue(11);
            lq.Enqueue(22);
            lq.Enqueue(33);
            lq.Enqueue(44);
            lq.Dequeue();
            lq.Enqueue(55);
            lq.Enqueue(11);
            lq.Enqueue(22);
            lq.Enqueue(33);
            lq.Enqueue(44);
            lq.Dequeue();
            lq.Enqueue(55);
            lq.Enqueue(11);
            lq.Enqueue(22);
            lq.Enqueue(33);
            lq.Enqueue(44);
            lq.Dequeue();
        }
    }
}
