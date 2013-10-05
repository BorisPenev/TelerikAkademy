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

namespace Calculator
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


        private void btnNumbers_OnClick(object sender, RoutedEventArgs e)
        {
            Button btnAction = sender as Button;
            if (btnAction != null)
            {
                txtCalculate.Text += btnAction.Content;
            }
        }

        private void btnSum_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCalculate.Text))
            {
                txtCalculate.Text = txtCalculate.Text.Split('+').Select(x => int.Parse(x)).ToList().Sum().ToString();

            }
        }

        private void btnClear_OnClick(object sender, RoutedEventArgs e)
        {
            txtCalculate.Text = string.Empty;
        }
    }
}
