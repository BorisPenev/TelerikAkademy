using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using CinemaReserve.ResponseModels;
using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Config;
using CinemaReserve.WpfClient.Data;
using CinemaReserve.WpfClient.Helpers;
using CinemaReserve.WpfClient.Interfaces;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class CinemaViewModel : PropertyChange, ITitleViewModel 
    {
        public string Title { get; private set; }

        public ObservableCollection<CinemaModel> Cinemas { get; set; }

        public CinemaViewModel()
        {
            Title = "All Cinemas";
            Cinemas = new ObservableCollection<CinemaModel>();

            AppCache.Config.IsBusyPool.Add("Get all cinemas, please wait");
            LoadData.GetAllCinemas(result =>
            {
                var cinemaResult = result as List<CinemaModel>;
                if (cinemaResult != null)
                {
                    cinemaResult.ForEach(c => Cinemas.Add(c));
                }
                else
                {
                    //this.ErrorMessage = result.ToString();
                }
                AppCache.Config.IsBusyPool.TryRemove("Get all cinemas, please wait");
            });
        }
    }
}