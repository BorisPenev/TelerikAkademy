using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Graphics.Printing;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Windows.UI.Xaml.Printing;
using WinRTXamlToolkit.IO.Extensions;

namespace PrintWebView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            RegisterForPrinting();
        }

        Image image = new Image();
        private async void BtnPrint_OnClick(object sender, RoutedEventArgs e)
        {
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            await mainWebView.CapturePreviewToStreamAsync(ms);
            BitmapSource small = await resize((int)mainWebView.ActualWidth, (int)mainWebView.ActualHeight, ms);
            image.Source = small;

            await PrintManager.ShowPrintUIAsync();
        }

        async Task<BitmapSource> resize(int width, int height, Windows.Storage.Streams.IRandomAccessStream source)
        {
            WriteableBitmap small = new WriteableBitmap(width, height);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(source);
            BitmapTransform transform = new BitmapTransform();
            transform.ScaledHeight = (uint)height;
            transform.ScaledWidth = (uint)width;
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

        #region print

        /// <summary>
        ///  Printing root property on each input page.
        /// </summary>
        private Canvas PrintingRoot
        {
            get
            {
                return FindName("printingRoot") as Canvas;
            }
        }

        /// <summary>
        /// Current preview page
        /// </summary>
        internal int currentPreviewPage;

        /// <summary>
        /// PrintDocument is a Xaml object which converts some PrintManager functionality into paradigms
        /// which are used in Xaml (eg. callbacks into events with event handlers).
        /// </summary>
        private PrintDocument printDocument = null;

        /// <summary>
        /// Marker interface for document source
        /// </summary>
        private IPrintDocumentSource printDocumentSource = null;

        /// <summary>
        /// A list of UIElements used to store the print preview pages.  This gives easy access
        /// to any desired preview page.
        /// </summary>
        internal List<PageLoadState> printPreviewPages = new List<PageLoadState>();

        /// <summary>
        /// The percent of app's margin width, content is set at 85% (0.85) of the area's width
        /// </summary>
        private const double ApplicationContentMarginLeft = 0.075;

        /// <summary>
        /// The percent of app's margin heigth, content is set at 94% (0.94) of tha area's height
        /// </summary>
        private const double ApplicationContentMarginTop = 0.03;

        /// <summary>
        /// Helper getter for text showing
        /// </summary>
        private bool ShowText
        {
            get { return ((int)imageText & (int)DisplayContent.Text) == (int)DisplayContent.Text; }
        }

        /// <summary>
        /// Helper getter for image showing
        /// </summary>
        private bool ShowImage
        {
            get { return ((int)imageText & (int)DisplayContent.Images) == (int)DisplayContent.Images; }
        }

        /// <summary>
        /// A flag that determines if text & images are to be shown
        /// </summary>
        internal DisplayContent imageText = DisplayContent.TextAndImages;

        /// <summary>
        /// This is the event handler for PrintManager.PrintTaskRequested.
        /// </summary>
        /// <param name="sender">PrintManager</param>
        /// <param name="e">PrintTaskRequestedEventArgs </param>
        private void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
        {
            PrintTask printTask = e.Request.CreatePrintTask("Hidden Truth", sourceRequested => sourceRequested.SetSource(printDocumentSource));
        }

        /// <summary>
        /// This function registers the app for printing with Windows and sets up the necessary event handlers for the print process.
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
        /// This function unregisters the app for printing with Windows.
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
        /// This is the event handler for PrintDocument.Paginate. It creates print preview pages for the app.
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
            PrintTaskOptions printingOptions = ((PrintTaskOptions)e.PrintTaskOptions);

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
        /// This is the event handler for PrintDocument.GetPrintPreviewPage. It provides a specific print preview page,
        /// in the form of an UIElement, to an instance of PrintDocument. PrintDocument subsequently converts the UIElement
        /// into a page that the Windows print system can deal with.
        /// </summary>
        /// <param name="sender">PrintDocument</param>
        /// <param name="e">Arguments containing the preview requested page</param>
        private void GetPrintPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            Interlocked.Exchange(ref currentPreviewPage, e.PageNumber - 1);

            PageLoadState pageLoadState = printPreviewPages[e.PageNumber - 1];

            // Set the preview even if images failed to load properly
            printDocument.SetPreviewPage(e.PageNumber, pageLoadState.Page);
        }

        /// <summary>
        /// This is the event handler for PrintDocument.AddPages. It provides all pages to be printed, in the form of
        /// UIElements, to an instance of PrintDocument. PrintDocument subsequently converts the UIElements
        /// into a pages that the Windows print system can deal with.
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
        /// This function creates and adds one print preview page to the internal cache of print preview
        /// pages stored in printPreviewPages.
        /// </summary>
        /// <param name="lastRTBOAdded">Last RichTextBlockOverflow element added in the current content</param>
        /// <param name="printPageDescription">Printer's page description</param>
        private RichTextBlockOverflow AddOnePrintPreviewPage(RichTextBlockOverflow lastRTBOAdded, PrintPageDescription printPageDescription)
        {
            // Create a cavase which represents the page 
            Canvas page = new Canvas();
            page.Width = printPageDescription.PageSize.Width;
            page.Height = printPageDescription.PageSize.Height;

            PageLoadState pageState = new PageLoadState(page, printPreviewPages.Count);
            pageState.ReadyAction = async (pageNumber, currentPage) =>
            {
                // Ignore if this is not the current page
                if (Interlocked.CompareExchange(ref currentPreviewPage, currentPreviewPage, pageNumber) == pageNumber)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        //await new Windows.UI.Popups.MessageDialog("Content loaded").ShowAsync();
                        printDocument.SetPreviewPage(pageNumber + 1, currentPage);
                    });
                }
            };

            // Create a grid which contains the actual content to be printed
            Grid content = new Grid();

            // Get the margins size
            // If the ImageableRect is smaller than the app provided margins use the ImageableRect
            double marginWidth = Math.Max(printPageDescription.PageSize.Width - printPageDescription.ImageableRect.Width,
                                        printPageDescription.PageSize.Width * ApplicationContentMarginLeft * 2);

            double marginHeight = Math.Max(printPageDescription.PageSize.Height - printPageDescription.ImageableRect.Height,
                                         printPageDescription.PageSize.Height * ApplicationContentMarginTop * 2);

            // Set content size based on the given margins
            content.Width = printPageDescription.PageSize.Width - marginWidth;
            content.Height = printPageDescription.PageSize.Height - marginHeight;

            // Set content margins
            content.SetValue(Canvas.LeftProperty, marginWidth / 2);
            content.SetValue(Canvas.TopProperty, marginHeight / 2);

            // If lastRTBOAdded is null then we know we are creating the first page. 
            bool isFirstPage = lastRTBOAdded == null;

            FrameworkElement previousLTCOnPage = null;
            RichTextBlockOverflow rtbo = new RichTextBlockOverflow();
            // Create the linked containers and and add them to the content grid
            if (isFirstPage)
            {
                // The first linked container in a chain of linked containers is is always a RichTextBlock
                if (ShowText)
                {
                    RichTextBlock rtbl = new RichTextBlock();
                    rtbl = AddContentToRTBl(rtbl);
                    content.Children.Add(rtbl);
                    // Save the RichTextBlock as the last linked container added to this page
                    previousLTCOnPage = rtbl;
                }
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
        /// This function adds content to the blocks collection of the RichTextBlock passed into the function.      
        /// </summary>
        /// <param name="rtbl">last rich text block</param>
        private RichTextBlock AddContentToRTBl(RichTextBlock rtbl)
        {
            InlineUIContainer container = new InlineUIContainer();
            container.Child = image;
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(container);
            rtbl.Blocks.Add(paragraph);
            return rtbl;
        }

        #endregion
    }
}
