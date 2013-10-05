using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Google.Apis.Blogger.v3.Data;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;

namespace TheHiddenTruth.Library.ViewModel
{
    public class SearchResultViewModel: ViewModelBase
    {
        private INavigationService _navigationService;
        private IServiceManager _serviceManager;
        private PageModel _searchResult;

        public PageModel SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                RaisePropertyChanged(() => SearchResult);
            }
        }

        public SearchResultViewModel(INavigationService navigationService, IServiceManager serviceManager)
        {
            _navigationService = navigationService;
            _serviceManager = serviceManager;
        }

        public string QueryText { get; set; }

        public async Task LoadData()
        {
            await _serviceManager.Search(this.QueryText, SearchResultResponse);
        }

        private void SearchResultResponse(PageModel pageModel, Exception exception)
        {
            if (exception != null)
            {
                System.Diagnostics.Debug.WriteLine(exception.ToString());
            }
            else
            {
                SearchResult = pageModel;
            }
        }
    }
}
