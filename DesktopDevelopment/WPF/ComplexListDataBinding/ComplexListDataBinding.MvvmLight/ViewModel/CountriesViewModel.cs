using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ComplexListDataBinding.Models;
using ComplexListDataBinding.MvvmLight.Commands;
using ComplexListDataBinding.MvvmLight.Model;
using GalaSoft.MvvmLight;

namespace ComplexListDataBinding.MvvmLight.ViewModel
{
    public class CountriesViewModel: ViewModelBase
    {
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; base.RaisePropertyChanged("SelectedItem"); }
        }

        private readonly ObservableCollection<Country> _countries = new ObservableCollection<Country>();
        public ObservableCollection<Country> Countries
        {
            get
            {
                if (_countries.Count == 0)
                {
                    _countries.Add(new Country()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bulgaria",
                        Flag = "http://th06.deviantart.net/fs48/PRE/i/2009/194/8/1/Bulgary_Grunge_Flag_by_think0.jpg",
                        Language = "Bulgarian",
                        Cities = new ObservableCollection<City>()
                        {
                            new City()
                            {
                                Id = 1,
                                Name = "Ruse",
                                Population = 149642
                            },
                            new City()
                            {
                                Id = 2,
                                Name = "Burgas",
                                Population = 200271
                            },
                        }
                    });
                    _countries.Add(new Country()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Greece",
                        Flag = "http://www.crwflags.com/fotw/images/g/gr.gif",
                        Language = "Greek",
                        Cities = new ObservableCollection<City>()
                        {
                            new City()
                            {
                                Id = 3,
                                Name = "Thessaloniki",
                                Population = 385406
                            },
                            new City()
                            {
                                Id = 4,
                                Name = "Rhodes",
                                Population = 80000
                            },
                        }
                    });
                }
                return _countries;
            }
        }

        public CountriesViewModel(IDataService data)
        {
            
        }

        bool isInsertMode = false;
        bool isBeingEdited = false;

        private ICommand _onRowEditEnding;
        public ICommand OnRowEditEnding
        {
            get
            {
                if (this._onRowEditEnding == null)
                {
                    this._onRowEditEnding = new RelayCommand(this.ExecuteOnRowEditEnding);
                }
                return this._onRowEditEnding;
            }
        }

        private void ExecuteOnRowEditEnding(object obj)
        {
            var e = obj as DataGridRowEditEndingEventArgs;

            var country = e.Row.DataContext as Country;
            if (isInsertMode && country != null)
            {
                var insertRecord = MessageBox.Show("Do you want to add " + country.Name + " as a new country?",
                    "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (insertRecord == MessageBoxResult.Yes)
                {
                    //var newCountry = new Country();
                    //newCountry.Name = country.Name;
                    //newCountry.Language = country.Language;
                    //newCountry.Flag = country.Flag;

                    //this.Countries.Add(newCountry);

                    //dgEmp.ItemsSource = GetEmployeeList();
                    MessageBox.Show(country.Name + " has being added!",
                        "Inserting Record", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.isInsertMode = false;
                }
                //else
                //    dgEmp.ItemsSource = GetEmployeeList();
            }
            //context.SaveChanges();
        }

        private ICommand _onAddingNewItem;

        public ICommand OnAddingNewItem
        {
            get
            {
                if (this._onAddingNewItem == null)
                {
                    this._onAddingNewItem = new RelayCommand(this.ExecuteOnAddingNewItem);
                }
                return this._onAddingNewItem;
            }
        }

        private void ExecuteOnAddingNewItem(object obj)
        {
            isInsertMode = true;
        }

        private ICommand _onBeginningEdit;

        public ICommand OnBeginningEdit
        {
            get
            {
                if (this._onBeginningEdit == null)
                {
                    this._onBeginningEdit = new RelayCommand(this.ExecuteOnBeginningEdit);
                }
                return this._onBeginningEdit;
            }
        }

        private void ExecuteOnBeginningEdit(object obj)
        {
            isBeingEdited = true;
        }

        private ICommand _onPreviewKeyDown;

        public ICommand OnPreviewKeyDown
        {
            get
            {
                if (this._onPreviewKeyDown == null)
                {
                    this._onPreviewKeyDown = new RelayCommand(this.ExecuteOnPreviewKeyDown);
                }
                return this._onPreviewKeyDown;
            }
        }

        private void ExecuteOnPreviewKeyDown(object obj)
        {
            var e = obj as KeyEventArgs;
            if (e.Key == Key.Delete && !isBeingEdited)
            {
                if (this.SelectedItem != null)
                {
                    var Res = MessageBox.Show("Are you sure you want to delete this Countries?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (Res == MessageBoxResult.Yes)
                    {
                        //List<Country> removedCountries = new List<Country>();
                        //foreach (var row in s)
                        //{
                        //    var country = this.SelectedItem as Country;
                        //    removedCountries.Add(country);
                        //}
                        //removedCountries.ForEach(c =>
                        //{
                        //    this.Countries.Remove(c);
                        //});
                        var country = this.SelectedItem as Country;
                        this.Countries.Remove(country);
                        //context.SaveChanges();
                        //MessageBox.Show(removedCountries.Count + " Countries have being deleted!");
                        //removedCountries.Clear();
                    }
                    //else
                    //    dgEmp.ItemsSource = GetEmployeeList();
                }
            }
        }
    }
}
