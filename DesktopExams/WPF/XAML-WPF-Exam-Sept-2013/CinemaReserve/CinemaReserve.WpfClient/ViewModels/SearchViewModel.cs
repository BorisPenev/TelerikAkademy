using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CinemaReserve.ResponseModels;
using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Config;
using CinemaReserve.WpfClient.Data;
using CinemaReserve.WpfClient.Helpers;
using CinemaReserve.WpfClient.Interfaces;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class SearchViewModel : PropertyChange, ITitleViewModel
    {
        public string Title { get; private set; }

        private ObservableCollection<MovieModel> _searchResult;
        
        public ObservableCollection<MovieModel> SearchResult
        {
            get { return _searchResult; }
            set
            {
                if (_searchResult != value)
                {
                    _searchResult = value;
                    RaisePropertyChanged("SearchResult");
                }
            }
        }

        public ICommand SearchCommand { get; set; }


        public SearchViewModel()
        {
            Title = "Search Movies";
            SearchResult = new ObservableCollection<MovieModel>();
            this.SearchCommand = new RelayCommand(this.ExecuteSearchCommand);
        }

        private void ExecuteSearchCommand(object parameters)
        {
            var e = parameters as KeyEventArgs;
            if (e != null && e.Key == Key.Enter)
            {
                var txtSearch = e.OriginalSource as TextBox;
                if (txtSearch != null)
                {
                    var searchQuery = txtSearch.Text;
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        SearchResult.Clear();
                        AppCache.Config.IsBusyPool.Add("Searching movies, please wait");
                        LoadData.Search(searchQuery, result =>
                        {
                            var cinemaResult = result as List<MovieModel>;
                            if (cinemaResult != null)
                            {
                                cinemaResult.ForEach(c => SearchResult.Add(c));
                            }
                            else
                            {
                                //this.ErrorMessage = result.ToString();
                            }
                            AppCache.Config.IsBusyPool.TryRemove("Searching movies, please wait");
                        });
                    }
                }
            }
        }

        
    }
}
