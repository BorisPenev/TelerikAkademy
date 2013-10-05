using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedDataBinding.PhoneStore.Library;

namespace AdvancedDataBinding.PhoneStore.Models
{
    public class StoreViewModel: PropertyChange
    {
        private string _name;
        public Guid Id { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public ObservableCollection<PhoneModel> Phones { get; set; }

        public StoreViewModel()
        {
            Phones = new ObservableCollection<PhoneModel>();
        }
    }
}
