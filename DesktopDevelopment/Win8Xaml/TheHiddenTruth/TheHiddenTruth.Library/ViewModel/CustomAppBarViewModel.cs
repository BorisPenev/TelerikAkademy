using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using TheHiddenTruth.Library.Services;

namespace TheHiddenTruth.Library.ViewModel
{
    public class CustomAppBarViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IServiceManager _serviceManager;
        private Uri _avatar;
        private double _latitude;
        private double _longitude;

        public Uri Avatar
        {
            get { return _avatar; }
            set
            {
                if (value != _avatar)
                {
                    _avatar = value;
                    RaisePropertyChanged(() => Avatar);
                }
            }
        }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (value != _latitude)
                {
                    _latitude = value;
                    RaisePropertyChanged(() => Latitude);
                }
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (value != _longitude)
                {
                    _longitude = value;
                    RaisePropertyChanged(() => Longitude);
                }
            }
        }

        public CustomAppBarViewModel(INavigationService navigationService, IServiceManager serviceManager)
        {
            _navigationService = navigationService;
            _serviceManager = serviceManager;
            _avatar = new Uri("http://i.imgur.com/ynquHtt.jpg");
        }
    }
}
