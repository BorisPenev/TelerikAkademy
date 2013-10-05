using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;

namespace TheHiddenTruth.Library.ViewModel
{
    public class PivotItemViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IServiceManager _serviceManager;
        private PageModel _currentPage;
        private ItemModel _selectedItem = new ItemModel();

        public PageModel CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged(() => CurrentPage);
            }
        }

        public const string SelectedItemPropertyName = "SelectedItem";

        public ItemModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (Set(SelectedItemPropertyName, ref _selectedItem, value)
                    && value != null)
                {
                    _navigationService.Navigate<ItemViewModel>(value.Id);
                }
            }
        }

        public PivotItemViewModel(INavigationService navigationService, IServiceManager serviceManager, PageModel currentPage)
        {
            _navigationService = navigationService;
            _serviceManager = serviceManager;
            _currentPage = currentPage;
        }
    }
}
