using System;
using System.Collections.ObjectModel;


namespace ComplexListDataBinding.Models
{
    public class Country : PropertyChange
    {
        private Guid _id = Guid.NewGuid();
        private string _name;
        private string _language;
        private string _flag;
        private ObservableCollection<City> _cities = new ObservableCollection<City>();

        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; RaisePropertyChanged("Language"); }
        }

        public string Flag
        {
            get { return _flag; }
            set { _flag = value; RaisePropertyChanged("Flag"); }
        }

        public ObservableCollection<City> Cities
        {
            get { return _cities; }
            set { _cities = value; }
        }
    }
}
