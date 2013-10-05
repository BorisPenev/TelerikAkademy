using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using ComplexListDataBinding.Data;
using ComplexListDataBinding.Models;
using ComplexListDataBinding.ProductStore.Commands;

namespace ComplexListDataBinding.ProductStore.ViewModels
{
    public class ProductsViewModel: PropertyChange
    {
        private readonly ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private readonly ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private Product _selectedProduct;
        private ICommand _getProductCommand;
        private ICommand _addProductCommand;
        private ICommand _clearFormCommand;

        public ObservableCollection<Category> Categories
        {
            get
            {
                if (_categories.Count == 0)
                {
                    var db = new ProductContext();
                    db.Categories.ToList().ForEach(category => _categories.Add(category));
                }
                return _categories;
            }
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                if (_products.Count == 0)
                {
                    var db = new ProductContext();
                    db.Products.ToList().ForEach(product => _products.Add(product));
                }
                return _products;
            }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; RaisePropertyChanged("SelectedProduct"); }
        }

        public ICommand GetProduct
        {
            get
            {
                if (this._getProductCommand == null)
                {
                    this._getProductCommand = new RelayCommand(this.ExecuteGetProductCommand);
                }
                return this._getProductCommand;
            }
        }

        private void ExecuteGetProductCommand(object productId)
        {
            if (productId != null)
            {
                var product = this.Products.FirstOrDefault(p => p.Id == int.Parse(productId.ToString()));
                if (product != null)
                {
                    this.SelectedProduct = product;
                }

            }
        }

        public ICommand AddProduct
        {
            get
            {
                if (this._addProductCommand == null)
                {
                    this._addProductCommand = new RelayCommand(this.ExecuteAddProductCommand);
                }
                return this._addProductCommand;
            }
        }

        private void ExecuteAddProductCommand(object obj)
        {
            if (!string.IsNullOrEmpty(this.SelectedProduct.ModelName) &&
                !string.IsNullOrEmpty(this.SelectedProduct.ModelNumber) &&
                !string.IsNullOrEmpty(this.SelectedProduct.Coast.ToString()) &&
                !string.IsNullOrEmpty(this.SelectedProduct.Desctiption))
            {
                var db = new ProductContext();
                db.Products.Add(this.SelectedProduct);
                db.SaveChanges();
                this.Products.Add(this.SelectedProduct);
            }
        }

        public ICommand ClearForm
        {
            get
            {
                if (this._clearFormCommand == null)
                {
                    this._clearFormCommand = new RelayCommand(this.ExecuteClearFormCommand);
                }
                return this._clearFormCommand;
            }
        }

        private void ExecuteClearFormCommand(object obj)
        {
            this.SelectedProduct = new Product();
        }

        //public void Next()
        //{
        //    var collection = this.GetDefaultView(this._countries);
        //    collection.MoveCurrentToNext();
        //    if (collection.IsCurrentAfterLast)
        //    {
        //        collection.MoveCurrentToLast();
        //    }
        //}

        //public void Prev()
        //{
        //    var collection = this.GetDefaultView(this._countries);
        //    collection.MoveCurrentToPrevious();
        //    if (collection.IsCurrentBeforeFirst)
        //    {
        //        collection.MoveCurrentToFirst();
        //    }
        //}

        //private ICollectionView GetDefaultView<T>(IEnumerable<T> collection)
        //{
        //    return CollectionViewSource.GetDefaultView(collection);
        //}
    }
}
