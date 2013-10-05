// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233
using System;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;
using TheHiddenTruth.Library.Utils;
using TheHiddenTruth.Library.ViewModel;
using TheHiddenTruth.Store.Common;
using MyToolkit.Controls;
using Newtonsoft.Json;
using WinRTXamlToolkit.IO.Extensions;

namespace TheHiddenTruth.Store.View
{
    /// <summary>
    ///     A page that displays a collection of item previews.  In the Split App this page
    ///     is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class PagesView : Page
    {
        private readonly NavigationHelper _navigationHelper;

        /// <summary>
        ///     NavigationHelper is used on each page to aid in navigation and
        ///     process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        public PagesView()
        {
            InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += navigationHelper_LoadState;
            _navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        ///     Populates the page with content passed during navigation.  Any saved state is also
        ///     provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        ///     The source of the event; typically <see cref="NavigationHelper" />
        /// </param>
        /// <param name="e">
        ///     Event data that provides both the navigation parameter passed to
        ///     <see cref="Frame.Navigate(Type, Object)" /> when this page was initially requested and
        ///     a dictionary of state preserved by this page during an earlier
        ///     session.  The state will be null the first time a page is visited.
        /// </param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null && e.PageState.ContainsKey("CurrentSite"))
            {
                var result = await StringIOExtensions.ReadFromFile("data.txt");
                var json = await JsonConvert.DeserializeObjectAsync<ObservableCollection<SiteModel>>(result);
                if (json != null)
                {
                    ServiceManager.Sites = json;
                }
                var ds = this.DataContext as PageViewModel;
                if (ds != null)
                {
                    await ds.LoadData(e.PageState["CurrentSite"].ToString(), null);
                    if (e.PageState.ContainsKey("SelectedIndex"))
                    {
                        ds.CurrentSite.SelectedIndex = e.PageState["SelectedIndex"].ToInt();
                        PnlPivot_OnLoaded(null,null);
                    }
                }
                e.PageState["CurrentSite"] = null;
                e.PageState["SelectedIndex"] = null;
            }
            else
            {
                var ds = DataContext as PageViewModel;
                if (ds != null)
                {
                    await ds.LoadData(e.NavigationParameter.ToString(), null);
                }
            }
        }

        /// <summary>
        ///     Preserves state associated with this page in case the application is suspended or the
        ///     page is discarded from the navigation cache.  Values must conform to the serialization
        ///     requirements of <see cref="SuspensionManager.SessionState" />.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper" /></param>
        /// <param name="e">
        ///     Event data that provides an empty dictionary to be populated with
        ///     serializable state.
        /// </param>
        private async void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            var ds = this.DataContext as PageViewModel;
            if (ds != null)
            {
                e.PageState["CurrentSite"] = ds.CurrentSite.Id;
                e.PageState["SelectedIndex"] = PnlPivot.SelectedIndex;
                var result = await JsonConvert.SerializeObjectAsync(ServiceManager.Sites);
                await StringIOExtensions.WriteToFile(result, "data.txt");
            }
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the
        /// <see cref="GridCS.Common.NavigationHelper.LoadState" />
        /// and
        /// <see cref="GridCS.Common.NavigationHelper.SaveState" />
        /// .
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var ds = DataContext as PageViewModel;
            if (ds != null && ds.CurrentSite != null)
            {
                ds.CurrentSite.SelectedIndex = PnlPivot.SelectedIndex;
            }
            
            _navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void PnlPivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = 0;
            if (sender is ExtendedListBox)
            {
                selectedIndex = (sender as ExtendedListBox).SelectedIndex;
            }
            
                var ds = DataContext as PageViewModel;
            if (ds != null)
            {
                if (ds.CurrentSite != null &&
                    selectedIndex == ds.CurrentSite.Pages.Count - 1)
                {
                    await ds.LoadData(ds.CurrentSite.Id,
                        ds.CurrentSite.Pages[selectedIndex].NextPageToken);
                    if (selectedIndex + 1 < ds.CurrentSite.Pages.Count)
                    {
                        int pageIndex = selectedIndex + 1;
                        AddPivotItem(pageIndex, ds);
                    }
                }
            }
        }

        private void PnlPivot_OnLoaded(object sender, RoutedEventArgs e)
        {
            var ds = DataContext as PageViewModel;
            if (ds != null)
            {
                if (ds.CurrentSite != null && ds.CurrentSite.Pages.Count > 0)
                {
                    var pivotItemTemplate = PnlPivot.Items[0].Content as PivotItemTemplate;
                    if (pivotItemTemplate != null)
                    {
                        pivotItemTemplate.DataContext = new PivotItemViewModel(ds._navigationService, ds._serviceManager,
                            ds.CurrentSite.Pages[0]);
                    }
                    if (PnlPivot.Items.Count < ds.CurrentSite.Pages.Count)
                    {
                        for (int i = PnlPivot.Items.Count; i < ds.CurrentSite.Pages.Count; i++)
                        {
                            AddPivotItem(i, ds);
                        }
                    }
                    if (ds.CurrentSite.SelectedIndex < PnlPivot.Items.Count)
                    {
                        PnlPivot.SelectedIndex = ds.CurrentSite.SelectedIndex;
                    }
                }
            }
        }

        private void AddPivotItem(int pageIndex, PageViewModel titleDetailsViewModel)
        {
            PnlPivot.Items.Add(new PivotItem
            {
                Content = new PivotItemTemplate
                {
                    DataContext =
                        new PivotItemViewModel(titleDetailsViewModel._navigationService,
                            titleDetailsViewModel._serviceManager, titleDetailsViewModel.CurrentSite.Pages[pageIndex])
                },
                Header = "Page" + (pageIndex + 1),
            });
        }

        public static string page;
        public static int counter = 1;
        private static Guid logoSecondaryTileId = Guid.NewGuid();
        public static string dynamicTileId = "SecondaryTile.LiveTile";
        public static string appbarTileId = "SecondaryTile.AppBar";

        private async void BtnPinSecondaryTile_OnClick(object sender, RoutedEventArgs e)
        {
            var logo = new Uri("ms-appx:///Assets/Images/150x150.png");
            var smallLogo = new Uri("ms-appx:///Assets/Images/310x150.png");

            // During creation of secondary tile, an application may set additional arguments on the tile that will be passed in during activation.
            // These arguments should be meaningful to the application. In this sample, we'll pass in the date and time the secondary tile was pinned.
            string tileActivationArguments = logoSecondaryTileId + " WasPinnedAt=" + DateTime.Now.ToLocalTime();
            counter++;
            // Create a 1x1 Secondary tile
            var s = new SecondaryTile(logoSecondaryTileId.ToString(),
                "Hidden Truth Blog page",
                "Hidden Truth Blog page",
                tileActivationArguments,
                TileOptions.ShowNameOnLogo,
                logo);
            s.DisplayName = "Hidden Truth";
            // Specify a foreground text value.
            s.ForegroundText = ForegroundText.Dark;
            bool isPinned =
                await s.RequestCreateForSelectionAsync(GetElementRect((FrameworkElement) sender), Placement.Below);
            // OK, the tile is created and we can now attempt to pin the tile.
            // Note that the status message is updated when the async operation to pin the tile completes.
        }

        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationHelper.GoBackCommand.Execute(sender);
        }
    }
}
