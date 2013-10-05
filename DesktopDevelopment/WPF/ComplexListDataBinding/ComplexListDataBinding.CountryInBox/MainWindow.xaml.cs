using System;
using System.Collections.Generic;
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
using ComplexListDataBinding.CountryInBox.ViewModels;

namespace ComplexListDataBinding.CountryInBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private CountriesViewModel GetDataContext()
        {
            var dataContext = this.DataContext;
            return dataContext as CountriesViewModel;
        }

        private void OnPrevButtonClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.GetDataContext();
            dataContext.Prev();
        }

        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.GetDataContext();
            dataContext.Next();
        }

        private void btnSave_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
