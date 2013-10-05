using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHiddenTruth.Store.Services.Helpers
{
    public class PrintServiceEventArgs : EventArgs
    {
        public PrintServiceEventArgs()
        { }

        public PrintServiceEventArgs(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// The message from the print service.
        /// </summary>
        public string Message { get; set; }
    }
}
