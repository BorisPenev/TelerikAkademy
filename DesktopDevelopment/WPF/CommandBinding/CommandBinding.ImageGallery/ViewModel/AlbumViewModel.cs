using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using CommandBinding.ImageGallery.Helpers;
using CommandBinding.ImageGallery.Models;

namespace CommandBinding.ImageGallery.ViewModel
{
    class AlbumViewModel: PropertyChange
    {
        private ICommand _addNewImageCommand;
        private AlbumModel _selectedAlbum = new AlbumModel();
        private ImageModel _selectedImage = new ImageModel();
        private ImageModel _newImage = new ImageModel();
        private RelayCommand _prevImageCommand;
        private RelayCommand _nextImageCommand;

        public ObservableCollection<AlbumModel> AlbumModels { get; set; }

        public AlbumModel SelectedAlbum
        {
            get { return _selectedAlbum; }
            set
            {
                if (_selectedAlbum != value)
                {
                    _selectedAlbum = value;
                    RaisePropertyChanged("SelectedAlbum");
                }
            }
        }

        public ImageModel SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                if (_selectedImage != value)
                {
                    _selectedImage = value;
                    RaisePropertyChanged("SelectedImage");
                }
            }
        }

        public ImageModel NewImage
        {
            get { return _newImage; }
            set
            {
                if (_newImage != value)
                {
                    _newImage = value;
                    RaisePropertyChanged("NewImage");
                }
            }
        }

        public AlbumViewModel()
        {
            AlbumModels = Utils.DeserializeFromXml<ObservableCollection<AlbumModel>>("Albums.xml");
            //InitData();
        }

        public ICommand AddNewImage
        {
            get
            {
                if (this._addNewImageCommand == null)
                {
                    this._addNewImageCommand = new RelayCommand(this.ExecuteAddNewImageCommand);
                }
                return this._addNewImageCommand;
            }
        }

        private void ExecuteAddNewImageCommand(object image)
        {
            this.SelectedAlbum.ImageModels.Add(this.NewImage);
            Utils.SerializeToXml(this.AlbumModels, "Albums.xml");
            this.NewImage = new ImageModel();
        }

        public ICommand NextImage
        {
            get
            {
                if (this._nextImageCommand == null)
                {
                    this._nextImageCommand = new RelayCommand(this.ExecuteNextImageCommand);
                }
                return this._nextImageCommand;
            }
        }

        public void ExecuteNextImageCommand(object obj)
        {
            var collection = this.GetDefaultView(this.SelectedAlbum.ImageModels);
            collection.MoveCurrentToNext();
            if (collection.IsCurrentAfterLast)
            {
                collection.MoveCurrentToLast();
            }
        }

        public ICommand PrevImage
        {
            get
            {
                if (this._prevImageCommand == null)
                {
                    this._prevImageCommand = new RelayCommand(this.ExecutePrevImageCommand);
                }
                return this._prevImageCommand;
            }
        }

        public void ExecutePrevImageCommand(object obj)
        {
            var collection = this.GetDefaultView(this.SelectedAlbum.ImageModels);
            collection.MoveCurrentToPrevious();
            if (collection.IsCurrentBeforeFirst)
            {
                collection.MoveCurrentToFirst();
            }
        }

        private ICollectionView GetDefaultView<T>(IEnumerable<T> collection)
        {
            return CollectionViewSource.GetDefaultView(collection);
        }

        private void InitData()
        {
            AlbumModels = new ObservableCollection<AlbumModel>();
            AlbumModels.Add(new AlbumModel()
            {
                Id = Guid.NewGuid(),
                ImageModels = new ObservableCollection<ImageModel>()
                {
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Star Wars 1",
                        ImageSource =
                            "http://2.bp.blogspot.com/-amTgFK88GKk/UMuVI-TytSI/AAAAAAAAA3I/ZDaUtdxKuCo/s1600/NEeDa303gbaDgh_1_2.jpg"
                    },
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Star Wars 2",
                        ImageSource =
                            "http://3.bp.blogspot.com/-gXilzP8tGMo/UJ4TkYyVVyI/AAAAAAAAALY/JcS16SNtigM/s400/star+wars+wallpaper+5.jpeg"
                    },
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "star-wars-revenge",
                        ImageSource =
                            "http://wac.450f.edgecastcdn.net/80450F/screencrush.com/files/2012/11/star-wars-revenge-of-the-sith-3d.jpg"
                    }
                }
            });

            AlbumModels.Add(new AlbumModel()
            {
                Id = Guid.NewGuid(),
                ImageModels = new ObservableCollection<ImageModel>()
                {
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Farscape 1",
                        ImageSource = "http://www.awkwardgeeks.com/wp-content/uploads/2013/05/Farscape-crew.jpg"
                    },
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Farscape 2",
                        ImageSource =
                            "http://images1.wikia.nocookie.net/__cb20130422123123/muppet/images/c/c8/Farscapecast0715.jpg"
                    },
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Farscape 3",
                        ImageSource = "http://tuningintoscifitv.com/wp-content/uploads/2012/10/Farscape-Crew.jpg"
                    }
                }
            });

            AlbumModels.Add(new AlbumModel()
            {
                Id = Guid.NewGuid(),
                ImageModels = new ObservableCollection<ImageModel>()
                {
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Stargate",
                        ImageSource = "http://images3.wikia.nocookie.net/__cb20080930035259/stargate/images/2/28/304.jpg"
                    },
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Stargate",
                        ImageSource =
                            "http://stargatetothecosmos.org/wp-content/uploads/2012/05/Stargate_Trinity___Wallpaper_by_mercscilla.jpg"
                    },
                    new ImageModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Stargate",
                        ImageSource =
                            "http://3.bp.blogspot.com/-bddUDfKKUWs/UM-nBJjvsRI/AAAAAAACNvw/hKcjlvKFnic/s150/Stargate_sg1_poster68.jpg"
                    }
                }
            });

            Utils.SerializeToXml(this.AlbumModels, "Albums.xml");
        }
    }
}
