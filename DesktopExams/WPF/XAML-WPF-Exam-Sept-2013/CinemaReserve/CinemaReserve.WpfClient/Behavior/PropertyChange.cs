using System.ComponentModel;

namespace CinemaReserve.WpfClient.Behavior
{
    public class PropertyChange : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
