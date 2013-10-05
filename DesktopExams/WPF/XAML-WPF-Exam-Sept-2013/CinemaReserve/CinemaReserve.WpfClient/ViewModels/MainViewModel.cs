using System.Collections.ObjectModel;
using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Models;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class MainViewModel : PropertyChange
    {
        private ObservableCollection<MenuItemModel> _menuItems = new ObservableCollection<MenuItemModel>();

        public ObservableCollection<MenuItemModel> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (_menuItems != value)
                {
                    _menuItems = value;
                    RaisePropertyChanged("MenuItems");
                }
            }
        }

        public MainViewModel()
        {
            MenuItems.Add(new MenuItemModel()
            {
                Name = "Cinemas",
                CurrentViewModel = new CinemaViewModel(),
                IsActive = true
            });
            MenuItems.Add(new MenuItemModel()
            {
                Name = "Search Movies",
                CurrentViewModel = new SearchViewModel(),
                IsActive = false
            });
        }
    }
}
