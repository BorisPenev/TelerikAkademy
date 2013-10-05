using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexListDataBinding.Models
{
    public class Product: PropertyChange
    {
        private int _id;
        private string _modelName;
        private string _modelNumber;
        private double _coast;
        private string _desctiption;
        private int _categoryId;

        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; RaisePropertyChanged("ModelName"); }
        }

        public string ModelNumber
        {
            get { return _modelNumber; }
            set { _modelNumber = value; RaisePropertyChanged("ModelNumber"); }
        }

        public double Coast
        {
            get { return _coast; }
            set { _coast = value; RaisePropertyChanged("Coast"); }
        }

        public string Desctiption
        {
            get { return _desctiption; }
            set { _desctiption = value; RaisePropertyChanged("Desctiption"); }
        }

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; RaisePropertyChanged("CategoryId"); }
        }

        public virtual Category Category { get; set; }

    }
}
