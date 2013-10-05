using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;

namespace TheHiddenTruth.Library.ViewModel
{
    public class ItemViewModel: ViewModelBase
    {
        private INavigationService _navigationService;
        private IServiceManager _serviceManager;
        private PageModel _currentPage;
        private ItemModel _selectedItem = new ItemModel();
        private bool _isPrintEnable;

        public RelayCommand DownloadPdfCommand { get; private set; }

        public bool IsPrintEnable
        {
            get { return _isPrintEnable; }
            set
            {
                _isPrintEnable = value;
                RaisePropertyChanged(() => IsPrintEnable);
            }
        }

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
                Set(SelectedItemPropertyName, ref _selectedItem, value);
            }
        }

        public ItemViewModel(INavigationService navigationService, IServiceManager serviceManager)
        {
            _navigationService = navigationService;
            _serviceManager = serviceManager;
            this.DownloadPdfCommand = new RelayCommand(this.ExecuteDownloadPdfCommand);
        }

        private void ExecuteDownloadPdfCommand()
        {

        }
    }
}
