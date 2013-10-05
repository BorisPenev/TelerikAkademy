using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedDataBinding.PhoneStore.Library;

namespace AdvancedDataBinding.PhoneStore.Models
{
    public class PhoneModel: PropertyChange
    {
        private string _vendor;
        private string _model;
        private int _yearOfProduction;
        private string _os;
        public Guid Id { get; set; }

        public string Vendor
        {
            get { return _vendor; }
            set
            {
                if (_vendor != value)
                {
                    _vendor = value;
                    RaisePropertyChanged("Vendor");
                }
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    RaisePropertyChanged("Model");
                }
            }
        }

        public int YearOfProduction
        {
            get { return _yearOfProduction; }
            set
            {
                if (_yearOfProduction != value)
                {
                    _yearOfProduction = value;
                    RaisePropertyChanged("YearOfProduction");
                }
            }
        }

        public string Os
        {
            get { return _os; }
            set
            {
                if (_os != value)
                {
                    _os = value;
                    RaisePropertyChanged("Os");
                }
            }
        }

        public ObservableCollection<PhoneFeaturesModel> Features { get; set; }

        public PhoneModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
