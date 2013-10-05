using System.Collections.ObjectModel;
using System.Linq;
using CinemaReserve.ResponseModels;
using CinemaReserve.WpfClient.Behavior;

namespace CinemaReserve.WpfClient.Config
{
    public class Configuration: PropertyChange
    {
        private CinemaModel _selectedCinema;

        public CinemaModel SelectedCinema
        {
            get { return _selectedCinema; }
            set
            {
                if (_selectedCinema != value)
                {
                    _selectedCinema = value;
                    RaisePropertyChanged("SelectedCinema");
                }
            }
        }

        #region BusyIndicator

        private ObservableCollection<string> _isBusyPool = new ObservableCollection<string>();
        public ObservableCollection<string> IsBusyPool
        {
            get { return _isBusyPool; }
            set
            {
                if (_isBusyPool != value)
                {
                    _isBusyPool = value;
                    RaisePropertyChanged("IsBusyPool");
                }
            }
        }

        private bool _showMessageBox = false;

        public bool ShowMessageBox
        {
            get { return _showMessageBox; }
            set
            {
                if (_showMessageBox != value)
                {
                    _showMessageBox = value;
                    RaisePropertyChanged("ShowMessageBox");
                }
            }
        }

        //private MessageBoxDataContext _messageBoxDataContext = new MessageBoxDataContext();
        //public MessageBoxDataContext MessageBoxDataContext
        //{
        //    get { return _messageBoxDataContext; }
        //    set
        //    {
        //        _messageBoxDataContext = value;
        //        RaisePropertyChanged("MessageBoxDataContext");
        //    }
        //}

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        private string _isBusyMessage = "Please Wait...";
        

        public string IsBusyMessage
        {
            get { return _isBusyMessage; }
            set
            {
                if (_isBusyMessage != value)
                {
                    _isBusyMessage = value;
                    RaisePropertyChanged("IsBusyMessage");
                }
            }
        }

        #endregion
       
    }
}