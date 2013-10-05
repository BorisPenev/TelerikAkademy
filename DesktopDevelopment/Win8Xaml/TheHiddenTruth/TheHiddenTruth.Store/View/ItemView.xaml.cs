// The Split Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234234
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.Graphics.Printing;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Printing;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;
using TheHiddenTruth.Library.Utils;
using TheHiddenTruth.Library.ViewModel;
using TheHiddenTruth.Store.Common;
using TheHiddenTruth.Store.Services.Helpers;
using Newtonsoft.Json;
using NotificationsExtensions.ToastContent;
using WinRTXamlToolkit.IO.Extensions;

namespace TheHiddenTruth.Store.View
{
    /// <summary>
    ///     A page that displays a group title, a list of items within the group, and details for
    ///     the currently selected item.
    /// </summary>
    public sealed partial class ItemView : Page
    {
        private readonly NavigationHelper _navigationHelper;
        private WebView _thisWebView;
        private DataTransferManager _dataTransferManager;
        private readonly Image image = new Image();

        /// <summary>
        ///     NavigationHelper is used on each page to aid in navigation and
        ///     process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        public ItemView()
        {
            InitializeComponent();

            // Setup the navigation helper
            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += navigationHelper_LoadState;
            _navigationHelper.SaveState += navigationHelper_SaveState;

            // Setup the logical page navigation components that allow
            // the page to only show one pane at a time.
            _navigationHelper.GoBackCommand = new RelayCommand(() => GoBack(), () => CanGoBack());
            itemListView.SelectionChanged += ItemListView_SelectionChanged;

            // Start listening for Window size changes 
            // to change from showing two panes to showing a single pane
            Window.Current.SizeChanged += Window_SizeChanged;
            InvalidateVisualState();
        }

        private void contentView_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            var ds = DataContext as ItemViewModel;
            if (ds != null)
            {
                ds.IsPrintEnable = false;
            }
        }
        
        private async void contentView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var ds = DataContext as ItemViewModel;
            if (ds != null)
            {
                ds.IsPrintEnable = true;
            }
        }

        private async Task<BitmapSource> resize(int width, int height, IRandomAccessStream source)
        {
            var small = new WriteableBitmap(width, height);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(source);
            var transform = new BitmapTransform();
            transform.ScaledHeight = (uint) height;
            transform.ScaledWidth = (uint) width;
            // transform.InterpolationMode = BitmapInterpolationMode.NearestNeighbor;
            PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.DoNotColorManage);
            pixelData.DetachPixelData().CopyTo(small.PixelBuffer);
            return small;
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
            if (e.PageState != null && e.PageState.ContainsKey("SelectedItem"))
            {
                var result = await StringIOExtensions.ReadFromFile("data.txt");
                ServiceManager.Sites = await JsonConvert.DeserializeObjectAsync<ObservableCollection<SiteModel>>(result);
                var ds = this.DataContext as ItemViewModel;
                if (ds != null)
                {
                    var itemId = e.PageState["SelectedItem"].ToString();
                    ds.CurrentPage = await ServiceManager.GetPageAsync(itemId);
                    ds.SelectedItem = await ServiceManager.GetItemAsync(itemId);
                }
                e.PageState["SelectedItem"] = null;
            }
            else
            {
                var itemId = e.NavigationParameter.ToString();
                PageModel selectedPage = await ServiceManager.GetPageAsync(itemId);
                if (selectedPage != null)
                {
                    var ds = DataContext as ItemViewModel;
                    if (ds != null)
                    {
                        ds.CurrentPage = selectedPage;
                        ds.SelectedItem = selectedPage.Items.FirstOrDefault(x => x.Id.Equals(itemId));
                    }
                }
            }
        }

        /// <summary>
        ///     Preserves state associated with this page in case the application is suspended or the
        ///     page is discarded from the navigation cache.  Values must conform to the serialization
        ///     requirements of <see cref="SuspensionManager.SessionState" />.
        /// </summary>
        /// <param name="navigationParameter">
        ///     The parameter value passed to
        ///     <see cref="Frame.Navigate(Type, Object)" /> when this page was initially requested.
        /// </param>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper" /></param>
        /// <param name="e">
        ///     Event data that provides an empty dictionary to be populated with
        ///     serializable state.
        /// </param>
        private async void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            var ds = this.DataContext as ItemViewModel;
            if (ds != null)
            {
                e.PageState["SelectedItem"] = ds.SelectedItem.Id;
                var result = await JsonConvert.SerializeObjectAsync(ServiceManager.Sites);
                await StringIOExtensions.WriteToFile(result, "data.txt");
            }
        }

        #region Logical page navigation

        // The split page isdesigned so that when the Window does have enough space to show
        // both the list and the dteails, only one pane will be shown at at time.
        //
        // This is all implemented with a single physical page that can represent two logical
        // pages.  The code below achieves this goal without making the user aware of the
        // distinction.

        private const int MinimumWidthForSupportingTwoPanes = 768;

        /// <summary>
        ///     Invoked to determine whether the page should act as one logical page or two.
        /// </summary>
        /// <returns>
        ///     True if the window should show act as one logical page, false
        ///     otherwise.
        /// </returns>
        private bool UsingLogicalPageNavigation()
        {
            return Window.Current.Bounds.Width < MinimumWidthForSupportingTwoPanes;
        }

        /// <summary>
        ///     Invoked with the Window changes size
        /// </summary>
        /// <param name="sender">The current Window</param>
        /// <param name="e">Event data that describes the new size of the Window</param>
        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            InvalidateVisualState();
        }

        /// <summary>
        ///     Invoked when an item within the list is selected.
        /// </summary>
        /// <param name="sender">The GridView displaying the selected item.</param>
        /// <param name="e">Event data that describes how the selection was changed.</param>
        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Invalidate the view state when logical page navigation is in effect, as a change
            // in selection may cause a corresponding change in the current logical page.  When
            // an item is selected this has the effect of changing from displaying the item list
            // to showing the selected item's details.  When the selection is cleared this has the
            // opposite effect.
            if (UsingLogicalPageNavigation()) InvalidateVisualState();
        }

        private bool CanGoBack()
        {
            if (UsingLogicalPageNavigation() && itemListView.SelectedItem != null)
            {
                return true;
            }
            return _navigationHelper.CanGoBack();
        }

        private void GoBack()
        {
            if (UsingLogicalPageNavigation() && itemListView.SelectedItem != null)
            {
                // When logical page navigation is in effect and there's a selected item that
                // item's details are currently displayed.  Clearing the selection will return to
                // the item list.  From the user's point of view this is a logical backward
                // navigation.
                itemListView.SelectedItem = null;
            }
            else
            {
                _navigationHelper.GoBack();
            }
        }

        private void InvalidateVisualState()
        {
            string visualState = DetermineVisualState();
            VisualStateManager.GoToState(this, visualState, false);
            _navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Invoked to determine the name of the visual state that corresponds to an application
        ///     view state.
        /// </summary>
        /// <returns>
        ///     The name of the desired visual state.  This is the same as the name of the
        ///     view state except when there is a selected item in portrait and snapped views where
        ///     this additional logical page is represented by adding a suffix of _Detail.
        /// </returns>
        private string DetermineVisualState()
        {
            if (!UsingLogicalPageNavigation())
                return "PrimaryView";

            // Update the back button's enabled state when the view state changes
            bool logicalPageBack = UsingLogicalPageNavigation() && itemListView.SelectedItem != null;

            return logicalPageBack ? "SinglePane_Detail" : "SinglePane";
        }

        #endregion

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
            RegisterForPrinting();

            // Register the current page as a share source.
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            UnregisterForPrinting();

            // Unregister the current page as a share source.
            _dataTransferManager.DataRequested -= OnDataRequested;

            _navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void BtnGoToOriginalUrl_OnClick(object sender, RoutedEventArgs e)
        {
            var btnAction = sender as AppBarButton;
            if (btnAction != null && btnAction.Tag != null)
            {
                var item = btnAction.Tag as ItemModel;
                if (item != null)
                {
                    await Launcher.LaunchUriAsync(new Uri(item.OriginalUrl));
                }
            }
        }

        private async void BtnPrint_OnClick(object sender, RoutedEventArgs e)
        {
            var ms = new InMemoryRandomAccessStream();
            await _thisWebView.CapturePreviewToStreamAsync(ms);
            BitmapSource small = await resize((int) _thisWebView.ActualWidth, (int) _thisWebView.ActualHeight, ms);
            image.Source = small;

            await PrintManager.ShowPrintUIAsync();
            //this.printServiceProvider = new PrintService();
            //this.printServiceProvider.RegisterForPrinting(this, typeof(ItemView), this.DataContext);
            //this.printServiceProvider.Print();
        }

        #region print

        /// <summary>
        ///     Printing root property on each input page.
        /// </summary>
        private Canvas PrintingRoot
        {
            get { return FindName("printingRoot") as Canvas; }
        }

        /// <summary>
        ///     Current preview page
        /// </summary>
        internal int currentPreviewPage;

        /// <summary>
        ///     PrintDocument is a Xaml object which converts some PrintManager functionality into paradigms
        ///     which are used in Xaml (eg. callbacks into events with event handlers).
        /// </summary>
        private PrintDocument printDocument;

        /// <summary>
        ///     Marker interface for document source
        /// </summary>
        private IPrintDocumentSource printDocumentSource;

        /// <summary>
        ///     A list of UIElements used to store the print preview pages.  This gives easy access
        ///     to any desired preview page.
        /// </summary>
        internal List<PageLoadState> printPreviewPages = new List<PageLoadState>();

        /// <summary>
        ///     The percent of app's margin width, content is set at 85% (0.85) of the area's width
        /// </summary>
        private const double ApplicationContentMarginLeft = 0.075;

        /// <summary>
        ///     The percent of app's margin heigth, content is set at 94% (0.94) of tha area's height
        /// </summary>
        private const double ApplicationContentMarginTop = 0.03;

        /// <summary>
        ///     Helper getter for text showing
        /// </summary>
        private bool ShowText
        {
            get { return ((int) imageText & (int) DisplayContent.Text) == (int) DisplayContent.Text; }
        }

        /// <summary>
        ///     Helper getter for image showing
        /// </summary>
        private bool ShowImage
        {
            get { return ((int) imageText & (int) DisplayContent.Images) == (int) DisplayContent.Images; }
        }

        /// <summary>
        ///     A flag that determines if text & images are to be shown
        /// </summary>
        internal DisplayContent imageText = DisplayContent.TextAndImages;

        /// <summary>
        ///     This is the event handler for PrintManager.PrintTaskRequested.
        /// </summary>
        /// <param name="sender">PrintManager</param>
        /// <param name="e">PrintTaskRequestedEventArgs </param>
        private void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
        {
            PrintTask printTask = e.Request.CreatePrintTask("Hidden Truth",
                sourceRequested => sourceRequested.SetSource(printDocumentSource));
        }

        /// <summary>
        ///     This function registers the app for printing with Windows and sets up the necessary event handlers for the print
        ///     process.
        /// </summary>
        private void RegisterForPrinting()
        {
            // Create the PrintDocument.
            printDocument = new PrintDocument();

            // Save the DocumentSource.
            printDocumentSource = printDocument.DocumentSource;

            // Add an event handler which creates preview pages.
            printDocument.Paginate += CreatePrintPreviewPages;

            // Add an event handler which provides a specified preview page.
            printDocument.GetPreviewPage += GetPrintPreviewPage;

            // Add an event handler which provides all final print pages.
            printDocument.AddPages += AddPrintPages;


            // Create a PrintManager and add a handler for printing initialization.
            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested += PrintTaskRequested;
        }

        /// <summary>
        ///     This function unregisters the app for printing with Windows.
        /// </summary>
        private void UnregisterForPrinting()
        {
            // Set the instance of the PrintDocument to null.
            printDocument = null;

            // Remove the handler for printing initialization.
            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested -= PrintTaskRequested;
        }

        private event EventHandler pagesCreated;

        /// <summary>
        ///     This is the event handler for PrintDocument.Paginate. It creates print preview pages for the app.
        /// </summary>
        /// <param name="sender">PrintDocument</param>
        /// <param name="e">Paginate Event Arguments</param>
        private void CreatePrintPreviewPages(object sender, PaginateEventArgs e)
        {
            // Clear the cache of preview pages 
            printPreviewPages.Clear();

            // Clear the printing root of preview pages
            PrintingRoot.Children.Clear();

            // This variable keeps track of the last RichTextBlockOverflow element that was added to a page which will be printed
            RichTextBlockOverflow lastRTBOOnPage;

            // Get the PrintTaskOptions
            PrintTaskOptions printingOptions = e.PrintTaskOptions;

            // Get the page description to deterimine how big the page is
            PrintPageDescription pageDescription = printingOptions.GetPageDescription(0);

            // We know there is at least one page to be printed. passing null as the first parameter to
            // AddOnePrintPreviewPage tells the function to add the first page.
            lastRTBOOnPage = AddOnePrintPreviewPage(null, pageDescription);

            // We know there are more pages to be added as long as the last RichTextBoxOverflow added to a print preview
            // page has extra content
            while (lastRTBOOnPage.HasOverflowContent)
            {
                lastRTBOOnPage = AddOnePrintPreviewPage(lastRTBOOnPage, pageDescription);
            }

            if (pagesCreated != null)
                pagesCreated.Invoke(printPreviewPages, null);

            // Report the number of preview pages created
            printDocument.SetPreviewPageCount(printPreviewPages.Count, PreviewPageCountType.Intermediate);
        }

        /// <summary>
        ///     This is the event handler for PrintDocument.GetPrintPreviewPage. It provides a specific print preview page,
        ///     in the form of an UIElement, to an instance of PrintDocument. PrintDocument subsequently converts the UIElement
        ///     into a page that the Windows print system can deal with.
        /// </summary>
        /// <param name="sender">PrintDocument</param>
        /// <param name="e">Arguments containing the preview requested page</param>
        private void GetPrintPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            Interlocked.Exchange(ref currentPreviewPage, e.PageNumber - 1);

            PageLoadState pageLoadState = printPreviewPages[e.PageNumber - 1];

            if (!pageLoadState.Ready)
            {
                // Notify the user that some content is not available yet
                // Apps may also opt to don't show preview untill everything is complete and just use await IsReadyAsync
                //rootPage.NotifyUser("Image loading not complete, previewing only text", NotifyType.ErrorMessage);
            }

            // Set the preview even if images failed to load properly
            printDocument.SetPreviewPage(e.PageNumber, pageLoadState.Page);
        }

        /// <summary>
        ///     This is the event handler for PrintDocument.AddPages. It provides all pages to be printed, in the form of
        ///     UIElements, to an instance of PrintDocument. PrintDocument subsequently converts the UIElements
        ///     into a pages that the Windows print system can deal with.
        /// </summary>
        /// <param name="sender">PrintDocument</param>
        /// <param name="e">Add page event arguments containing a print task options reference</param>
        private void AddPrintPages(object sender, AddPagesEventArgs e)
        {
            // Loop over all of the preview pages and add each one to  add each page to be printied
            for (int i = 0; i < printPreviewPages.Count; i++)
            {
                // We should have all pages ready at this point...
                printDocument.AddPage(printPreviewPages[i].Page);
            }

            // Indicate that all of the print pages have been provided
            printDocument.AddPagesComplete();
        }

        /// <summary>
        ///     This function creates and adds one print preview page to the internal cache of print preview
        ///     pages stored in printPreviewPages.
        /// </summary>
        /// <param name="lastRTBOAdded">Last RichTextBlockOverflow element added in the current content</param>
        /// <param name="printPageDescription">Printer's page description</param>
        private RichTextBlockOverflow AddOnePrintPreviewPage(RichTextBlockOverflow lastRTBOAdded,
            PrintPageDescription printPageDescription)
        {
            // Create a cavase which represents the page 
            var page = new Canvas();
            page.Width = printPageDescription.PageSize.Width;
            page.Height = printPageDescription.PageSize.Height;

            var pageState = new PageLoadState(page, printPreviewPages.Count);
            pageState.ReadyAction = async (pageNumber, currentPage) =>
            {
                // Ignore if this is not the current page
                if (Interlocked.CompareExchange(ref currentPreviewPage, currentPreviewPage, pageNumber) == pageNumber)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        //await new Windows.UI.Popups.MessageDialog("Content loaded").ShowAsync();
                        printDocument.SetPreviewPage(pageNumber + 1, currentPage);
                    });
                }
            };

            // Create a grid which contains the actual content to be printed
            var content = new Grid();

            // Get the margins size
            // If the ImageableRect is smaller than the app provided margins use the ImageableRect
            double marginWidth = Math.Max(
                printPageDescription.PageSize.Width - printPageDescription.ImageableRect.Width,
                printPageDescription.PageSize.Width*ApplicationContentMarginLeft*2);

            double marginHeight =
                Math.Max(printPageDescription.PageSize.Height - printPageDescription.ImageableRect.Height,
                    printPageDescription.PageSize.Height*ApplicationContentMarginTop*2);

            // Set content size based on the given margins
            content.Width = printPageDescription.PageSize.Width - marginWidth;
            content.Height = printPageDescription.PageSize.Height - marginHeight;

            // Set content margins
            content.SetValue(Canvas.LeftProperty, marginWidth/2);
            content.SetValue(Canvas.TopProperty, marginHeight/2);

            //// Add the RowDefinitions to the Grid which is a content to be printed
            //RowDefinition rowDef = new RowDefinition();
            //rowDef.Height = new GridLength(0.7, GridUnitType.Star);
            //content.RowDefinitions.Add(rowDef);
            //rowDef = new RowDefinition();
            //rowDef.Height = new GridLength(0.8, GridUnitType.Star);
            //content.RowDefinitions.Add(rowDef);
            //rowDef = new RowDefinition();
            //rowDef.Height = new GridLength(2.5, GridUnitType.Star);
            //content.RowDefinitions.Add(rowDef);
            //rowDef = new RowDefinition();
            //rowDef.Height = new GridLength(3.5, GridUnitType.Star);
            //content.RowDefinitions.Add(rowDef);
            //rowDef = new RowDefinition();
            //rowDef.Height = new GridLength(1.5, GridUnitType.Star);
            //content.RowDefinitions.Add(rowDef);
            //rowDef = new RowDefinition();
            //rowDef.Height = new GridLength(0.5, GridUnitType.Star);
            //content.RowDefinitions.Add(rowDef);
            //rowDef = new RowDefinition();
            //rowDef.Height = new GridLength(0.5, GridUnitType.Star);
            //content.RowDefinitions.Add(rowDef);


            // Add the ColumnDefinitions to the Grid which is a content to be printed
            //ColumnDefinition colDef = new ColumnDefinition();
            //colDef.Width = new GridLength(40, GridUnitType.Star);
            //content.ColumnDefinitions.Add(colDef);
            //colDef = new ColumnDefinition();
            //colDef.Width = new GridLength(60, GridUnitType.Star);
            //content.ColumnDefinitions.Add(colDef);


            //// Create the "Windows 8 SDK Sample" header which consists of an image and text in a stack panel
            //// and add it to the content grid
            //Image windowsLogo = new Image();
            //windowsLogo.Source = new BitmapImage(new Uri("ms-appx:///Images/windows-sdk.png"));
            //pageState.ListenForCompletion((BitmapImage)windowsLogo.Source);

            //TextBlock headerText = new TextBlock();
            //headerText.TextWrapping = TextWrapping.Wrap;
            //headerText.Text = "Windows 8 SDK Sample";
            //headerText.FontSize = 20;
            //headerText.Foreground = new SolidColorBrush(Colors.Black);

            //StackPanel sp = new StackPanel();
            //sp.Orientation = Orientation.Horizontal;
            //sp.Children.Add(windowsLogo);
            //sp.Children.Add(headerText);


            //StackPanel outerPanel = new StackPanel();
            //outerPanel.Orientation = Orientation.Vertical;
            //outerPanel.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            //outerPanel.Children.Add(sp);
            //outerPanel.SetValue(Grid.RowProperty, 1);
            //outerPanel.SetValue(Grid.ColumnSpanProperty, 2);


            //TextBlock sampleTitle = new TextBlock();
            //sampleTitle.TextWrapping = TextWrapping.Wrap;
            //sampleTitle.Text = "Print Sample";
            //sampleTitle.FontSize = 22;
            //sampleTitle.FontWeight = FontWeights.Bold;
            //sampleTitle.Foreground = new SolidColorBrush(Colors.Black);
            //outerPanel.Children.Add(sampleTitle);

            //content.Children.Add(outerPanel);


            //// Create Microsoft image used to end each page and add it to the content grid
            //Image microsoftLogo = new Image();
            //microsoftLogo.Source = new BitmapImage(new Uri("ms-appx:///Images/microsoft-sdk.png"));
            //pageState.ListenForCompletion((BitmapImage)microsoftLogo.Source);
            //microsoftLogo.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            //microsoftLogo.SetValue(Grid.RowProperty, 5);
            //content.Children.Add(microsoftLogo);


            //// Add the copywrite notice and add it to the content grid
            //TextBlock copyrightNotice = new TextBlock();
            //copyrightNotice.Text = "© 2011 Microsoft. All rights reserved.";
            //copyrightNotice.FontSize = 16;
            //copyrightNotice.TextWrapping = TextWrapping.Wrap;
            //copyrightNotice.Foreground = new SolidColorBrush(Colors.Black);
            //copyrightNotice.SetValue(Grid.RowProperty, 6);
            //copyrightNotice.SetValue(Grid.ColumnSpanProperty, 2);
            //content.Children.Add(copyrightNotice);


            // If lastRTBOAdded is null then we know we are creating the first page. 
            bool isFirstPage = lastRTBOAdded == null;

            FrameworkElement previousLTCOnPage = null;
            var rtbo = new RichTextBlockOverflow();
            // Create the linked containers and and add them to the content grid
            if (isFirstPage)
            {
                // The first linked container in a chain of linked containers is is always a RichTextBlock
                if (ShowText)
                {
                    var rtbl = new RichTextBlock();
                    rtbl = AddContentToRTBl(rtbl);
                    content.Children.Add(rtbl);
                    // Save the RichTextBlock as the last linked container added to this page
                    previousLTCOnPage = rtbl;
                }

                //if (ShowImage)
                //{
                //    // Add the image to the first page and add it to the content grid
                //    Image pic = new Image();
                //    BitmapImage bitmap = new BitmapImage(new Uri("ms-appx:///Images/print_1.png"));
                //    pageState.ListenForCompletion(bitmap);
                //    pic.Source = bitmap;
                //    pic.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                //    pic.Margin = new Thickness(10);
                //    if (ShowText)
                //    {
                //        pic.SetValue(Grid.RowProperty, 3);
                //        pic.SetValue(Grid.ColumnProperty, 2);
                //        content.Children.Add(pic);
                //        content.RowDefinitions[2].Height = new GridLength(2.5, GridUnitType.Star);
                //    }
                //    else
                //    {
                //        pic.Width = content.Width;
                //        pic.Height = content.Height;
                //        page.Children.Add(pic);
                //    }
                //}
            }
            else if (ShowText)
            {
                // This is not the first page so the first element on this page has to be a
                // RichTextBoxOverflow that links to the last RichTextBlockOverflow added to
                // the previous page.
                rtbo = new RichTextBlockOverflow();
                rtbo.SetValue(Grid.RowProperty, 2);
                rtbo.SetValue(Grid.ColumnSpanProperty, 2);
                content.Children.Add(rtbo);

                // Keep text flowing from the previous page to this page by setting the linked text container just
                // created (rtbo) as the OverflowContentTarget for the last linked text container from the previous page 
                lastRTBOAdded.OverflowContentTarget = rtbo;

                // Save the RichTextBlockOverflow as the last linked container added to this page
                previousLTCOnPage = rtbo;
            }


            //if (ShowText)
            //{
            //    // Create the next linked text container for on this page.
            //    rtbo = new RichTextBlockOverflow();
            //    rtbo.SetValue(Grid.RowProperty, 3);

            //    // If this linked container is not on the first page make it span 2 columns.
            //    if (!isFirstPage || !ShowImage)
            //        rtbo.SetValue(Grid.ColumnSpanProperty, 2);

            //    // Add the RichTextBlockOverflow to the content to be printed.
            //    content.Children.Add(rtbo);

            //    // Add the new RichTextBlockOverflow to the chain of linked text containers. To do this we much check
            //    // to see if the previous container is a RichTextBlock or RichTextBlockOverflow.
            //    if (previousLTCOnPage is RichTextBlock)
            //        ((RichTextBlock)previousLTCOnPage).OverflowContentTarget = rtbo;
            //    else
            //        ((RichTextBlockOverflow)previousLTCOnPage).OverflowContentTarget = rtbo;

            //    // Save the last linked text container added to the chain
            //    previousLTCOnPage = rtbo;

            //    // Create the next linked text container for on this page.
            //    rtbo = new RichTextBlockOverflow();
            //    rtbo.SetValue(Grid.RowProperty, 4);
            //    rtbo.SetValue(Grid.ColumnSpanProperty, 2);
            //    content.Children.Add(rtbo);

            //    // Add the new RichTextBlockOverflow to the chain of linked text containers. We don't have to check
            //    // the type of the previous linked container this time because we know it's a RichTextBlockOverflow element
            //    ((RichTextBlockOverflow)previousLTCOnPage).OverflowContentTarget = rtbo;
            //}
            // We are done creating the content for this page. Add it to the Canvas which represents the page
            page.Children.Add(content);

            // Add the newley created page to the printing root which is part of the visual tree and force it to go
            // through layout so that the linked containers correctly distribute the content inside them.
            PrintingRoot.Children.Add(page);
            PrintingRoot.InvalidateMeasure();
            PrintingRoot.UpdateLayout();

            // Add the newley created page to the list of pages
            printPreviewPages.Add(pageState);

            // Return the last linked container added to the page
            return rtbo;
        }

        /// <summary>
        ///     This function adds content to the blocks collection of the RichTextBlock passed into the function.
        /// </summary>
        /// <param name="rtbl">last rich text block</param>
        private RichTextBlock AddContentToRTBl(RichTextBlock rtbl)
        {
            var container = new InlineUIContainer();
            container.Child = image;
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(container);
            //contentView.Visibility = Visibility.Collapsed;

            //// Create a Run and give it content
            //Run run = new Run();
            //run.Text = Reb.Text;
            //// Create a paragraph, set it's font size, add the run to the paragraph's inline collection
            //Paragraph para = new Paragraph();
            //para.FontSize = 42;
            //para.Inlines.Add(run);
            //// Add the paragraph to the blocks collection of the RichTextBlock
            rtbl.Blocks.Add(paragraph);
            return rtbl;
        }

        #endregion

        private async void BtnFacebook_OnClick(object sender, RoutedEventArgs e)
        {
            var btnAction = sender as AppBarButton;
            if (btnAction != null && btnAction.Tag != null)
            {
                var item = btnAction.Tag as ItemModel;
                if (item != null)
                {
                    await Launcher.LaunchUriAsync(new Uri("http://www.facebook.com/sharer.php?u=" + item.OriginalUrl));
                }
            }
        }

        private async void BtnTwitter_OnClick(object sender, RoutedEventArgs e)
        {
            var btnAction = sender as AppBarButton;
            if (btnAction != null && btnAction.Tag != null)
            {
                var item = btnAction.Tag as ItemModel;
                if (item != null)
                {
                    await Launcher.LaunchUriAsync(new Uri("http://twitter.com/home?status=" + item.OriginalUrl));
                }
            }
        }

        private async void BtnGoogle_OnClick(object sender, RoutedEventArgs e)
        {
            var btnAction = sender as AppBarButton;
            if (btnAction != null && btnAction.Tag != null)
            {
                var item = btnAction.Tag as ItemModel;
                if (item != null)
                {
                    await Launcher.LaunchUriAsync(new Uri("https://plus.google.com/share?url=" + item.OriginalUrl));
                }
            }
        }

        private void ContentView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _thisWebView = sender as WebView;
        }

        private async void BtnDownloadImage_OnClick(object sender, RoutedEventArgs e)
        {
            var ms = new InMemoryRandomAccessStream();
            await _thisWebView.CapturePreviewToStreamAsync(ms);

            // Launch file picker
            var picker = new FileSavePicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeChoices.Add("JPeg", new List<string> {".jpg", ".jpeg"});
            StorageFile file = await picker.PickSaveFileAsync();

            if (file == null)
                return;

            using (IRandomAccessStream fileStream1 = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await RandomAccessStream.CopyAndCloseAsync(ms.GetInputStreamAt(0), fileStream1.GetOutputStreamAt(0));
                DisplayTextToast("Файлът е записан успешно.");
            }
        }

        private const String TOAST_IMAGE_SRC = "Assets/Images/toast.png";

        private void DisplayTextToast(string message)
        {
            // Creates a toast using the notification object model, which is another project
            // in this solution.  For an example using Xml manipulation, see the function
            // DisplayToastUsingXmlManipulation below.
            IToastNotificationContent toastContent = null;

            IToastImageAndText01 templateContent = ToastContentFactory.CreateToastImageAndText01();
            templateContent.TextBodyWrap.Text = "Body text that wraps";
            templateContent.Image.Src = TOAST_IMAGE_SRC;
            templateContent.TextBodyWrap.Text = message;
            toastContent = templateContent;


            // Create a toast, then create a ToastNotifier object to show
            // the toast
            ToastNotification toast = toastContent.CreateNotification();

            // If you have other applications in your package, you can specify the AppId of
            // the app to create a ToastNotifier for that application
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private ItemModel ShareItem = new ItemModel();

        private void BtnSahreWebPage_OnClick(object sender, RoutedEventArgs e)
        {
            var btnAction = sender as AppBarButton;
            if (btnAction != null && btnAction.Tag != null)
            {
                var item = btnAction.Tag as ItemModel;
                if (item != null)
                {
                    ShareItem = item;
                    DataTransferManager.ShowShareUI();
                }
            }
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            // Call the scenario specific function to populate the datapackage with the data to be shared.
            if (GetShareContent(e.Request))
            {
                // Out of the datapackage properties, the title is required. If the scenario completed successfully, we need
                // to make sure the title is valid since the sample scenario gets the title from the user.
                if (String.IsNullOrEmpty(e.Request.Data.Properties.Title))
                {
                    //e.Request.FailWithDisplayText(MainPage.MissingTitleError);
                }
            }
        }

        private bool GetShareContent(DataRequest request)
        {
            bool succeeded = false;

            //The URI used in this sample is provided by the user so we need to ensure it's a well formatted absolute URI
            //before we try to share it.
            Uri dataPackageUri = ValidateAndGetUri(ShareItem.OriginalUrl);
            if (dataPackageUri != null)
            {
                DataPackage requestData = request.Data;
                requestData.Properties.Title = ShareItem.Title;
                requestData.Properties.ContentSourceApplicationLink = ApplicationLink;
                requestData.SetWebLink(dataPackageUri);
                succeeded = true;
            }
            else
            {
                request.FailWithDisplayText("Enter the web link you would like to share and try again.");
            }
            return succeeded;
        }

        private Uri ApplicationLink
        {
            get { return GetApplicationLink(GetType().Name); }
        }

        public static Uri GetApplicationLink(string sharePageName)
        {
            return new Uri("ms-sdk-sharesourcecs:navigate?page=" + sharePageName);
        }

        private Uri ValidateAndGetUri(string uriString)
        {
            Uri uri = null;
            try
            {
                uri = new Uri(uriString);
            }
            catch (FormatException)
            {
            }
            return uri;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationHelper.GoBackCommand.Execute(sender);
        }
    }
}