using GalaSoft.MvvmLight.Messaging;

namespace HiddenTruth.Library.Services.Navigation
{
    public class MessageSender<T> where T : InitMessageBase
    {
        private T _message = default(T);

        public MessageSender()
        {
            // Send back item when a constructor asks for it
            Messenger.Default.Register<T>(this, message =>
            {
                if (this._message != null && this._message != message && !this._message.Received)
                {
                    // Send back original message
                    Messenger.Default.Send(this._message);
                }
            });
        }

        public void SendMessage(T message)
        {
            // Store value
            this._message = message;

            // Try and send message
            Messenger.Default.Send(message);
        }
    }
}
