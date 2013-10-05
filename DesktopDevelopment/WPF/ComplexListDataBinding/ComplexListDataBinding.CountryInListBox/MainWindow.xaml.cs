using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComplexListDataBinding.CountryInListBox.ViewModels;
using ComplexListDataBinding.Models;

namespace ComplexListDataBinding.CountryInListBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isInsertMode = false;
        bool isBeingEdited = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private CountriesViewModel GetDataContext()
        {
            var dataContext = this.DataContext;
            return dataContext as CountriesViewModel;
        }

        private void GrdCountries_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var country = e.Row.DataContext as Country;
            if (isInsertMode && country != null)
            {
                var insertRecord = MessageBox.Show("Do you want to add " + country.Name + " as a new country?",
                    "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (insertRecord == MessageBoxResult.Yes)
                {
                    var newCountry = new Country();
                    newCountry.Name = country.Name;
                    newCountry.Language = country.Language;
                    newCountry.Flag = country.Flag;

                    GetDataContext().Countries.Add(newCountry);
                    
                    //dgEmp.ItemsSource = GetEmployeeList();
                    MessageBox.Show(country.Name + " has being added!",
                        "Inserting Record", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //else
                //    dgEmp.ItemsSource = GetEmployeeList();
            }
            //context.SaveChanges();
        }

        private void GrdCountries_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode = true;
        }

        private void GrdCountries_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited = true;
        }

        private void GrdCountries_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !isBeingEdited)
            {
                var grid = (DataGrid)sender;
                if (grid.SelectedItems.Count > 0)
                {
                    var Res = MessageBox.Show("Are you sure you want to delete " + grid.SelectedItems.Count + " Countries?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (Res == MessageBoxResult.Yes)
                    {
                        List<Country> removedCountries = new List<Country>();
                        foreach (var row in grid.SelectedItems)
                        {
                            Country country = row as Country;
                            removedCountries.Add(country);
                            
                        }
                        removedCountries.ForEach(c =>
                        {
                            GetDataContext().Countries.Remove(c);
                        });
                        //context.SaveChanges();
                        MessageBox.Show(removedCountries.Count + " Countries have being deleted!");
                        removedCountries.Clear();
                    }
                    //else
                    //    dgEmp.ItemsSource = GetEmployeeList();
                }
            }
        }
    }
}
