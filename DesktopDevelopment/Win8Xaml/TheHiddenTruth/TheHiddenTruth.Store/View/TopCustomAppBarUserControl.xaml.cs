using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using TheHiddenTruth.Library.ViewModel;
using MyToolkit.Controls;
using MyToolkit.Utilities;
using Parse;
using Flyout = Windows.UI.Xaml.Controls.Flyout;

namespace TheHiddenTruth.Store.View
{
    public sealed partial class TopCustomAppBarUserControl : UserControl
    {
        private Geolocator _geolocator = null;
        FlyoutUserControl f;

        public TopCustomAppBarUserControl()
        {
            this.InitializeComponent();
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("avatarLocalFile"))
            {
                var accessListEntries = StorageApplicationPermissions.FutureAccessList.Entries;
                var file =
                    StorageApplicationPermissions.FutureAccessList.GetFileAsync(
                        localSettings.Values["avatarLocalFile"].ToString()).RunSynchronouslyWithResult();
                AssignAvatar(file);
            }
            else if (localSettings.Values.ContainsKey("avatarParseFile"))
            {
                AssigneAvatarToModel(new Uri(localSettings.Values["avatarParseFile"].ToString()));
            }
            _geolocator = new Geolocator();
            f = new FlyoutUserControl();
            _geolocator.PositionChanged += new TypedEventHandler<Geolocator, PositionChangedEventArgs>(OnPositionChanged);
        }

        #region Geoposition

        /// <summary>
        /// This is the event handler for PositionChanged events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Geoposition pos = e.Position;
                var ds = this.DataContext as CustomAppBarViewModel;
                if (ds != null)
                {
                    ds.Latitude = pos.Coordinate.Point.Position.Latitude;
                    ds.Longitude = pos.Coordinate.Point.Position.Longitude;
                }
            });
        }

        #endregion

        private async void BtnAvatarFromCam_OnClick(object sender, RoutedEventArgs e)
        {
            var ui = new CameraCaptureUI();
            ui.PhotoSettings.CroppedAspectRatio = new Size(4, 3);

            var file = await ui.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (file != null)
            {
                IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var parseObject = new ParseFile(Guid.NewGuid() + ".jpg", fileStream.AsStreamForRead());
                await parseObject.SaveAsync();

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["avatarLocalFile"] = null;
                localSettings.Values["avatarParseFile"] = parseObject.Url.ToString();

                AssigneAvatarToModel(parseObject.Url);
            }
        }

        private void AssigneAvatarToModel(Uri fileUri)
        {
            var ds = this.DataContext as CustomAppBarViewModel;
            if (ds != null)
            {
                ds.Avatar = fileUri;
            }
        }

        private async void BtnAvatarFromFile_OnClick(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
           
            var file = await openPicker.PickSingleFileAsync();
            var falKey = StorageApplicationPermissions.FutureAccessList.Add(file);

            await AssignAvatar(file);

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["avatarLocalFile"] = falKey;
            localSettings.Values["avatarParseFile"] = null;
        }

        private async Task AssignAvatar(StorageFile file)
        {
            var image = new BitmapImage();
            var stream = await file.OpenAsync(FileAccessMode.Read);
            image.SetSource(stream);
            imgAvatar.Source = image;
        }
    }
}
