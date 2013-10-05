using System;
using System.Linq;
using ComplexListDataBinding.Data;

namespace ComplexListDataBinding.MvvmLight.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
                var db = new ProductContext();
                var categories = db.Categories.ToList();

            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }
    }
}