using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CinemaReserve.ResponseModels;
using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Config;
using CinemaReserve.WpfClient.Data;
using CinemaReserve.WpfClient.Helpers;
using CinemaReserve.WpfClient.Interfaces;
using CinemaReserve.WpfClient.Views;
using Telerik.Windows.Controls;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class MovieViewModel : PropertyChange, ITitleViewModel
    {
        private MovieModel _selectedMovie = new MovieModel();
        private MovieDetailsModel _movieDetails;
        private CinemaModel _selectedCinema;
        private ProjectionModel _selectedProjection;
        private ProjectionDetailsModel _projectionDetails;
        private RelayCommand _showAllMoviesCommand;
        private SeatModel _selectReservation;
        private bool _isCancelReservation;
        private string _reservationCancelToken;
        private RelayCommand _processCancelReservation;
        private RelayCommand _stopCancelReservation;
        private RelayCommand _closeMessage;
        private bool _isShowMessage;
        private string _reservationToken;
        private string _userEmail;

        public string Title { get; private set; }

        public ObservableCollection<MovieModel> Movies { get; set; }

        public ObservableCollection<ProjectionModel> Projections { get; set; }

        public ICommand SelectedCommand { get; set; }

        #region ShowAllMoviesCommand

        public ICommand ShowAllMoviesCommand
        {
            get
            {
                if (this._showAllMoviesCommand == null)
                {
                    this._showAllMoviesCommand = new RelayCommand(this.ExecuteShowAllMoviesCommand);
                }
                return this._showAllMoviesCommand;
            }
        }

        private void ExecuteShowAllMoviesCommand(object obj)
        {
            AppCache.Config.IsBusyPool.Add("Get all movies, please wait");
            LoadData.GetAllMovies(result =>
            {
                Movies.Clear();
                SelectedCinema = null;
                SelectedMovie = null;
                SelectedProjection = null;
                SelectReservation = null;
                Projections.Clear();
                var moviesResult = result as List<MovieModel>;
                if (moviesResult != null)
                {
                    moviesResult.ForEach(c => Movies.Add(c));
                }
                else
                {
                    //this.ErrorMessage = result.ToString();
                }
                AppCache.Config.IsBusyPool.TryRemove("Get all movies, please wait");
            });
        }

        #endregion

        #region ProcessCancelReservation
        public ICommand ProcessCancelReservation
        {
            get
            {
                if (this._processCancelReservation == null)
                {
                    this._processCancelReservation = new RelayCommand(this.ExecuteProcessCancelReservation);
                }
                return this._processCancelReservation;
            }
        }

        private void ExecuteProcessCancelReservation(object obj)
        {
            if (!string.IsNullOrEmpty(ReservationCancelToken) && !string.IsNullOrEmpty(UserEmail))
            {
                AppCache.Config.IsBusyPool.Add("Select Reservation, please wait");

                var cancelReservation = new RejectReservationModel()
                {
                    Email = UserEmail,
                    UserCode = ReservationCancelToken
                };
                LoadData.CancelReservation(SelectedProjection.Id, cancelReservation, result =>
                {
                    ExecuteStopCancelReservation(null);
                    ReservationCancelToken = string.Empty;
                    LoadProjection();
                    AppCache.Config.IsBusyPool.TryRemove("Select Reservation, please wait");
                });
            }
        }

        #endregion

        #region StopCancelReservation
        public ICommand StopCancelReservation
        {
            get
            {
                if (this._stopCancelReservation == null)
                {
                    this._stopCancelReservation = new RelayCommand(this.ExecuteStopCancelReservation);
                }
                return this._stopCancelReservation;
            }
        }

        private void ExecuteStopCancelReservation(object obj)
        {
            this.IsCancelReservation = false;
        }

        #endregion

        public MovieModel SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                if (_selectedMovie != value)
                {
                    _selectedMovie = value;
                    RaisePropertyChanged("SelectedMovie");
                    this.GetMovieDetails();
                }
            }
        }

        public SeatModel SelectReservation
        {
            get { return _selectReservation; }
            set
            {
                if (_selectReservation != value)
                {
                    _selectReservation = value;
                    RaisePropertyChanged("SelectReservation");
                    if (SelectReservation != null)
                    {
                        if (SelectReservation.Status == "free")
                        {
                            CreateReservation();
                        }
                        else
                        {
                            StartCancelReservation();
                        }
                    }
                }
            }
        }

        public bool IsCancelReservation
        {
            get { return _isCancelReservation; }
            set
            {
                if (_isCancelReservation != value)
                {
                    _isCancelReservation = value;
                    RaisePropertyChanged("IsCancelReservation");
                }
            }
        }

        public bool IsShowMessage
        {
            get { return _isShowMessage; }
            set
            {
                if (_isShowMessage != value)
                {
                    _isShowMessage = value;
                    RaisePropertyChanged("IsShowMessage");
                }
            }
        }

        public string ReservationCancelToken
        {
            get { return _reservationCancelToken; }
            set
            {
                if (_reservationCancelToken != value)
                {
                    _reservationCancelToken = value;
                    RaisePropertyChanged("ReservationCancelToken");
                }
            }
        }

        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                if (_userEmail != value)
                {
                    _userEmail = value;
                    RaisePropertyChanged("UserEmail");
                }
            }
        }

        public string ReservationToken
        {
            get { return _reservationToken; }
            set
            {
                if (_reservationToken != value)
                {
                    _reservationToken = value;
                    RaisePropertyChanged("ReservationToken");
                }
            }
        }

        private void StartCancelReservation()
        {
            this.IsCancelReservation = true;
        }

        private void CreateReservation()
        {
            AppCache.Config.IsBusyPool.Add("Select Reservation, please wait");
            var createReservation = new CreateReservationModel()
            {
                Email = "none@none.no",
                Seats = new List<SeatModel>()
                {
                    SelectReservation
                }
            };
            LoadData.Reservation(SelectedProjection.Id, createReservation, result =>
            {
                var moviesResult = result as ReservationModel;
                if (SelectedProjection != null && moviesResult != null)
                {
                    IsShowMessage = true;
                    ReservationToken = moviesResult.UserCode;
                    LoadProjection();
                }
                AppCache.Config.IsBusyPool.TryRemove("Select Reservation, please wait");
            });
        }

        #region CloseMessage
        public ICommand CloseMessage
        {
            get
            {
                if (this._closeMessage == null)
                {
                    this._closeMessage = new RelayCommand(this.ExecuteCloseMessage);
                }
                return this._closeMessage;
            }
        }

        private void ExecuteCloseMessage(object obj)
        {
            this.IsShowMessage = false;
        }

        #endregion

        public MovieDetailsModel MovieDetails
        {
            get { return _movieDetails; }
            set
            {
                if (_movieDetails != value)
                {
                    _movieDetails = value;
                    RaisePropertyChanged("MovieDetails");
                }
            }
        }

        public ProjectionModel SelectedProjection
        {
            get { return _selectedProjection; }
            set
            {
                if (_selectedProjection != value)
                {
                    _selectedProjection = value;
                    RaisePropertyChanged("SelectedProjection");
                    if (SelectedProjection != null)
                    {
                        LoadProjection();
                    }
                }
            }
        }

        private void LoadProjection()
        {
            AppCache.Config.IsBusyPool.Add("Get projection, please wait");
            LoadData.GetProjectionById(SelectedProjection.Id, result =>
            {
                var moviesResult = result as ProjectionDetailsModel;
                if (moviesResult != null)
                {
                    this.ProjectionDetails = moviesResult;
                }
                AppCache.Config.IsBusyPool.TryRemove("Get projection, please wait");
            });
        }

        public ProjectionDetailsModel ProjectionDetails
        {
            get { return _projectionDetails; }
            set
            {
                if (_projectionDetails != value)
                {
                    _projectionDetails = value;
                    RaisePropertyChanged("ProjectionDetails");
                }
            }
        }

        public CinemaModel SelectedCinema
        {
            get { return _selectedCinema; }
            set
            {
                if (_selectedCinema != value)
                {
                    _selectedCinema = value;
                    AppCache.Config.SelectedCinema = value;
                    
                    if (value != null)
                    {
                        this.MovieDetails = null;
                        LoadMoviesByCinemaId(SelectedCinema.Id);
                    }
                    RaisePropertyChanged("SelectedCinema");
                }
            }
        }

        private void GetMovieDetails()
        {
            if (SelectedMovie != null)
            {
                if (SelectedCinema != null)
                {
                    AppCache.Config.IsBusyPool.Add("Get Projection details, please wait");
                    LoadData.GetProjection(SelectedCinema.Id, SelectedMovie.Id, result =>
                    {
                        Projections.Clear();
                        var moviesResult = result as List<ProjectionModel>;

                        if (moviesResult != null)
                        {
                            moviesResult.ForEach(m => Projections.Add(m));
                        }
                        else
                        {
                            //this.ErrorMessage = result.ToString();
                        }
                        AppCache.Config.IsBusyPool.TryRemove("Get Projection details, please wait");
                    });
                }
                else
                {
                    AppCache.Config.IsBusyPool.Add("Get movie details, please wait");
                    LoadData.GetMoviesById(SelectedMovie.Id, result =>
                    {
                        var moviesResult = result as MovieDetailsModel;
                        if (moviesResult != null)
                        {
                            MovieDetails = moviesResult;
                        }
                        else
                        {
                            //this.ErrorMessage = result.ToString();
                        }
                        AppCache.Config.IsBusyPool.TryRemove("Get movie details, please wait");
                    });
                }
                
            }
        }

        public MovieViewModel()
        {
            Title = "All movies";
            Movies = new ObservableCollection<MovieModel>();
            Projections = new ObservableCollection<ProjectionModel>();
            SelectedCommand = new RelayCommand(this.ExecuteSelectedCommand);

            if (SelectedCinema != null)
            {
                if (SelectedCinema.Id > 0)
                {
                    LoadMoviesByCinemaId(SelectedCinema.Id);
                }
                
                
            }
        }

        private void LoadMoviesByCinemaId(int cinemaId)
        {
            AppCache.Config.IsBusyPool.Add("Get all movies, please wait");
            Movies.Clear();
            LoadData.GetMoviesByCinemaId(cinemaId, result =>
            {
                var moviesResult = result as List<MovieModel>;
                if (moviesResult != null)
                {
                    moviesResult.ForEach(c => Movies.Add(c));
                }
                else
                {
                    //this.ErrorMessage = result.ToString();
                }
                AppCache.Config.IsBusyPool.TryRemove("Get all movies, please wait");
            });
        }

        private void ExecuteSelectedCommand(object obj)
        {
            var e = obj as SelectionChangedEventArgs;
            if (e != null && e.AddedItems.Count > 0)
            {
                var pm = e.AddedItems[0] as ProjectionModel;
                if (pm != null)
                {
                    if (SelectedCinema == null)
                    {
                        
                        AppCache.Config.IsBusyPool.Add("Get projection, please wait");
                        LoadData.GetProjectionById(pm.Id, result =>
                        {
                            var moviesResult = result as List<MovieModel>;
                            if (moviesResult != null)
                            {
                                moviesResult.ForEach(c => Movies.Add(c));
                            }
                            else
                            {
                                //this.ErrorMessage = result.ToString();
                            }
                            AppCache.Config.IsBusyPool.TryRemove("Get projection, please wait");
                        });
                    }
                }
            }
        }
    }
}