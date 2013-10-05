using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Search;
using Windows.UI.Notifications;
using TheHiddenTruth.Store.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Split App template is documented at http://go.microsoft.com/fwlink/?LinkId=234228
using TheHiddenTruth.Store.View;
using NotificationsExtensions.TileContent;
using Parse;

namespace TheHiddenTruth.Store
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            App.Current.RequestedTheme = ApplicationTheme.Light;
            this.Suspending += OnSuspending;
            ParseClient.Initialize("uitRZ57uZs6MSj0NJky0HpUTsjgnZz4WHtJGpxou", "XKwGODuZLB8efwOIJrN5OwcQcJCHxYpgPmr3ghFz");
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage)))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        protected override async void OnWindowCreated(WindowCreatedEventArgs args)
        {
            
            base.OnWindowCreated(args);
            SearchPane.GetForCurrentView().ShowOnKeyboardInput = true;
            SearchPane.GetForCurrentView().QuerySubmitted += App_QuerySubmitted;

            //TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            //TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            //TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForSquare150x150(true);
            //TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForWide310x150(true);
            //TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueueForSquare310x310(true);

            //AddInitTiles();
        }

        private static void AddInitTiles()
        {
            // Note: This sample contains an additional project, NotificationsExtensions.
            // NotificationsExtensions exposes an object model for creating notifications, but you can also 
            // modify the strings directly. See UpdateTileWithImageWithStringManipulation_Click for an example

            // Users can resize tiles to large (Square310x310), wide (Wide310x150), medium (Square150x150) or small (Square70x70).
            // Apps can choose not to support all tile sizes (i.e. the app's tile can prevent being resized to wide or medium)
            // Supporting a large (Square310x310) tile requires supporting wide (Wide310x150) tile.

            // This sample application supports a Square150x150, Wide310x150 and Square310x310 Start tile. 
            // The user may have selected any of those sizes for their custom Start screen layout, so each 
            // notification should include template bindings for each supported tile size. (The Square70x70 tile
            // size does not support receiving live tile notifications, so we don't need a binding for that size.)
            // We assemble one notification with three template bindings by including the content for each smaller
            // tile in the next size up. Square310x310 includes Wide310x150, which includes Square150x150.
            // If we leave off the content for a tile size which the application supports, the user will not see the
            // notification if the tile is set to that size.

            // Create notification square310x310 content based on a visual template.
            ITileSquare310x310Image tileContent = TileContentFactory.CreateTileSquare310x310Image();
            tileContent.Image.Src = "ms-appx:///Assets/Images/310x310.png";
            tileContent.Image.Alt = "Hidden Truth";

            // create the notification for a wide310x150 template.
            ITileWide310x150ImageAndText01 wide310x150Content = TileContentFactory.CreateTileWide310x150ImageAndText01();
            wide310x150Content.TextCaptionWrap.Text = "Hidden Truth";
            wide310x150Content.Image.Src = "ms-appx:///Assets/Images/310x150.png";
            wide310x150Content.Image.Alt = "Hidden Truth";

            // create the square150x150 template and attach it to the wide310x150 template.
            ITileSquare150x150Image square150x150Content = TileContentFactory.CreateTileSquare150x150Image();
            square150x150Content.Image.Src = "ms-appx:///Assets/Images/150x150.png";
            square150x150Content.Image.Alt = "Hidden Truth";

            // add the square150x150 template to the wide310x150 template.
            wide310x150Content.Square150x150Content = square150x150Content;

            // add the wide310x150 to the Square310x310 template.
            tileContent.Wide310x150Content = wide310x150Content;

            // send the notification to the app's application tile.
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
        }


        void App_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;
            if (frame != null)
            {
                frame.Navigate(typeof(SearchResultView), args.QueryText);
                Window.Current.Content = frame;

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }
    }
}
