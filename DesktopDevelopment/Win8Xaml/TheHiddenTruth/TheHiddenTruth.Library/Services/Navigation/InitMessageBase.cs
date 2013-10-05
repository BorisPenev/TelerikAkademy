using GalaSoft.MvvmLight.Messaging;

namespace HiddenTruth.Library.Services.Navigation
{
    public abstract class InitMessageBase : MessageBase
    {
        // Used for a send 
        public bool Received { get; set; }

        public InitMessageBase()
        {

        }
    }
}
