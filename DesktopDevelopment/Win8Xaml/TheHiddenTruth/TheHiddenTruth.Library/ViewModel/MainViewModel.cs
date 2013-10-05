using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TheHiddenTruth.Library.Helpers;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;
using TheHiddenTruth.Library.Utils;

namespace TheHiddenTruth.Library.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IServiceManager _serviceManager;
        private PageModel _currentPage;
        private ItemModel _selectedItem = new ItemModel();

        public RelayCommand HeaderClickCommand { get; set; }

        private RelayCommand<string> _goToCommand;
        public RelayCommand<string> GoToCommand
        {
            get
            {
                if (_goToCommand == null)
                {
                    _goToCommand = new RelayCommand<string>(NavigateAway);
                }
                return _goToCommand;
            }
        }

        private void NavigateAway(string parameter)
        {
            _navigationService.Navigate<PageViewModel>(parameter);
        }

        public PageModel CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (value != _currentPage)
                {
                    _currentPage = value;
                    RaisePropertyChanged(() => CurrentPage);
                }
            }
        }

        public ObservableCollection<SiteModel> Sites
        {
            get
            {
                return ServiceManager.Sites;
            }
        } 

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService, IServiceManager serviceManager)
        {
            _navigationService = navigationService;
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// The <see cref="SelectedItem" /> property's name.
        /// </summary>
        public const string SelectedItemPropertyName = "SelectedItem";

        /// <summary>
        /// Sets and gets the SelectedFriend property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ItemModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (Set(SelectedItemPropertyName, ref _selectedItem, value) && value != null)
                {
                    _navigationService.Navigate<ItemViewModel>(value.Id);
                }
            }
        }

        public async Task LoadData(string pageToken)
        {
            await _serviceManager.GetDataBlogZaSeriozniHora(pageToken, (model, err) =>
            {
                if (err != null)
                {
                    System.Diagnostics.Debug.WriteLine(err.ToString());
                }
            });

            await _serviceManager.GetDataAlterInformation(pageToken.ToInt(1), (model, exception) =>
            {
                if (exception != null)
                {
                    System.Diagnostics.Debug.WriteLine(exception.ToString());
                }
            });
        }
    }
}