using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AdvancedDataBinding.PhoneStore.Library;
using AdvancedDataBinding.PhoneStore.Models;
using AdvancedDataBinding.PhoneStore.UserControls;

namespace AdvancedDataBinding.PhoneStore.VIewModels
{
    public class PhoneStoreViewModel: PropertyChange
    {
        private StoreViewModel _currentStore;
        private RelayCommand _addStoreCommand;
        private RelayCommand _clearStoreCommand;
        private RelayCommand _deleteStoreCommand;
        public ObservableCollection<StoreViewModel> Stores { get; set; }

        public List<PhoneFeaturesModel> Features { get; set; }

        public StoreViewModel CurrentStore
        {
            get { return _currentStore; }
            set
            {
                if (_currentStore != value)
                {
                    _currentStore = value;
                    RaisePropertyChanged("CurrentStore");
                }
            }
        }

        public PhoneStoreViewModel()
        {
            CurrentStore = new StoreViewModel();
            Features = new List<PhoneFeaturesModel>();
            Features.Add(new PhoneFeaturesModel()
            {
                Name = "Display Type: Super LCD3 capacitive touchscreen, 16M colors"
            });

            Features.Add(new PhoneFeaturesModel()
            {
                Name = "Display Size: 1080 x 1920 pixels, 4.7 inches (~469 ppi pixel density)"
            });

            Features.Add(new PhoneFeaturesModel()
            {
                Name = "Bluetooth: v4.0 with A2DP"
            });

            Features.Add(new PhoneFeaturesModel()
            {
                Name = "WLAN: Wi-Fi 802.11 a/ac/b/g/n, Wi-Fi Direct, DLNA, Wi-Fi hotspot"
            });

            Stores = new ObservableCollection<StoreViewModel>();
            Stores.Add(new StoreViewModel()
            {
                Name = "gsm now",
                Phones = new ObservableCollection<PhoneModel>
                {
                    new PhoneModel
                    {
                        Model = "One",
                        Vendor = "HTC",
                        Os = "Android OS, v4.1.2 (Jelly Bean), upgradable to v4.2.2 (Jelly Bean)",
                        YearOfProduction = 2012,
                    }
                }
            });
        }

        public ICommand ClearStoreCommand
        {
            get
            {
                if (this._clearStoreCommand == null)
                {
                    this._clearStoreCommand = new RelayCommand(this.ExecuteClearStoreCommand);
                }
                return this._clearStoreCommand;
            }
        }

        private void ExecuteClearStoreCommand(object parameters)
        {
            this.CurrentStore = new StoreViewModel();
        }

        public ICommand AddStoreCommand
        {
            get
            {
                if (this._addStoreCommand == null)
                {
                    this._addStoreCommand = new RelayCommand(this.ExecuteAddStoreCommand);
                }
                return this._addStoreCommand;
            }
        }

        private void ExecuteAddStoreCommand(object parameters)
        {
            this.Stores.Add(this.CurrentStore);
            ExecuteClearStoreCommand(null);
        }

        public ICommand DeleteStoreCommand
        {
            get
            {
                if (this._deleteStoreCommand == null)
                {
                    this._deleteStoreCommand = new RelayCommand(this.ExecuteDeleteStoreCommand);
                }
                return this._deleteStoreCommand;
            }
        }

        private void ExecuteDeleteStoreCommand(object parameters)
        {
            this.Stores.Remove(parameters as StoreViewModel);
            ExecuteClearStoreCommand(null);
        }
    }
}
