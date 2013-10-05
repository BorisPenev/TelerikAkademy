using System.Windows.Input;
using CinemaReserve.WpfClient.Interfaces;

namespace CinemaReserve.WpfClient.Models
{
    public class MenuItemModel
    {
        public string Name { get; set; }
        public ICommand ActivateCommand { get; set; }
        public bool IsActive { get; set; }

        public ITitleViewModel CurrentViewModel { get; set; }

    }
}
