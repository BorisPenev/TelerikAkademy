using System.ComponentModel;

namespace CommandBinding.ImageGallery.Helpers
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
