using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace TheHiddenTruth.Store.Services.Helpers
{
    /// <summary>
    /// Helpers class that tracks the loading state of a xaml element
    /// Elements which use async load patterns must enlist for observation
    /// </summary>
    public class PageLoadState
    {
        /// <summary>
        /// Event used to detect when all "loadable" elements are ready
        /// </summary>
        private CountdownEvent loadingElements;

        /// <summary>
        /// An action to execute when content is available (eg: SetPreview for the page)
        /// </summary>
        public Action<int, UIElement> ReadyAction { get; set; }

        /// <summary>
        /// Current page number in print/preview list
        /// </summary>
        private int pageNumber;

        /// <summary>
        /// XAML Page(element)
        /// </summary>
        private UIElement page;

        public UIElement Page
        {
            get { return page; }
        }

        public PageLoadState(UIElement page, int pageNumber)
        {
            this.page = page;
            this.pageNumber = pageNumber;
            loadingElements = new CountdownEvent(0);
        }

        /// <summary>
        /// Internal method that is called when an element has finished loading
        /// </summary>        
        private void SetElementComplete()
        {
            loadingElements.Signal();
        }

        /// <summary>
        /// Adds an element in the observation list
        /// </summary>
        /// <param name="bitmap">The bitmap on which to listen for ImageOpened event</param>
        public void ListenForCompletion(BitmapImage bitmap)
        {
            if (loadingElements.CurrentCount == 0)
            {
                // Event is already signaled. Manually set the count to 1 and "arm" the event.
                loadingElements.Reset(1);
            }
            else
            {
                // AddCount will throw if event is already in signaled state.
                loadingElements.AddCount();
            }
            bitmap.ImageOpened += (s, e) => SetElementComplete();
        }

        /// <summary>
        /// Property used to determine if the content is ready
        /// If content is not ready a background task will serve the content once it's ready
        /// </summary>
        public bool Ready
        {
            get
            {
                var ready = loadingElements.CurrentCount == 0;
                if (!ready)
                {
                    // A request was made and the content is not ready, serve it once it's complete
                    Task.Run(async () =>
                    {
                        await IsReadyAsync();
                        ReadyAction(pageNumber, page);
                    });
                }

                return ready;
            }
        }

        /// <summary>
        /// Async method that enables listening for the completion event in a background thread
        /// </summary>
        public async Task IsReadyAsync()
        {
            await Task.Run(() => { loadingElements.Wait(); });
        }
    }
}
