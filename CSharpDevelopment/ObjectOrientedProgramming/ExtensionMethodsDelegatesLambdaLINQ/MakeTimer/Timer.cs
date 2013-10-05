using System;
using System.Linq;
using System.Threading;
using System.Timers;

namespace MakeTimer
{
    public class Timer
    {
        public delegate void Execute();
        public event ElapsedEventHandler Elapsed;
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static CancellationTokenSource tokenSourceEvent = new CancellationTokenSource();

        private int miliseconds = 0;
        public int Miliseconds
        {
            get { return miliseconds; }
            set { miliseconds = value; }
        }

        private int interval = 0;
        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        private Execute executeCommand;
        public Execute ExecuteCommand
        {
            get { return executeCommand; }
            set { executeCommand = value; }
        }

        public Timer(int miliseconds)
        {
            this.Miliseconds = miliseconds;
        }

        //http://msdn.microsoft.com/en-us/library/dd997364.aspx
        //this method start a new thread in the program so this not block the main thread
        public void Start()
        {
            tokenSource = new CancellationTokenSource();
            tokenSource.Token.ThrowIfCancellationRequested();
            ThreadPool.QueueUserWorkItem(new WaitCallback(token =>
                {
                    DateTime start = DateTime.Now;
                    DateTime end = start.AddMilliseconds(this.Miliseconds);
                    while (start <= end || !((CancellationToken)token).IsCancellationRequested)
                    {
                        ExecuteCommand();
                        Thread.Sleep(Interval);
                        start = DateTime.Now;
                    }
                }), tokenSource.Token);
        }

        public void Stop()
        {
            tokenSource.Cancel();
            tokenSourceEvent.Cancel();
        }

        public void StartEvent()
        {
            tokenSourceEvent = new CancellationTokenSource();
            tokenSourceEvent.Token.ThrowIfCancellationRequested();
            ThreadPool.QueueUserWorkItem(new WaitCallback(token =>
                {
                    DateTime start = DateTime.Now;
                    DateTime end = start.AddMilliseconds(this.Miliseconds);
                    while (start <= end || !((CancellationToken)token).IsCancellationRequested)
                    {
                        if (Elapsed != null)
                            Elapsed(this, null);
                        Thread.Sleep(Interval);
                        start = DateTime.Now;
                    }
                }), tokenSourceEvent.Token);
        }
    }
}