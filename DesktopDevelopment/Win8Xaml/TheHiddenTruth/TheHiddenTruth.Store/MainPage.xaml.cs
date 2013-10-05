using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Newtonsoft.Json;
using TheHiddenTruth.BackgroundTasks;
using TheHiddenTruth.Library.Services;
using TheHiddenTruth.Library.ViewModel;
using TheHiddenTruth.Store.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using WinRTXamlToolkit.IO.Extensions;

namespace TheHiddenTruth.Store
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string TASKNAMEUSERPRESENT = "TileSchedulerTaskUserPresent";
        private const string TASKNAMETIMER = "TileSchedulerTaskTimer";
        private const string TASKENTRYPOINT = "TheHiddenTruth.BackgroundTasks.TileSchedulerTask";

        //private const string TASK_NAME = "TileUpdater";
        //private const string TASK_ENTRY = "TheHiddenTruth.BackgroundTasks.TileUpdater";
        private readonly NavigationHelper _navigationHelper;

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this._navigationHelper; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this._navigationHelper = new NavigationHelper(this);
            this._navigationHelper.LoadState += navigationHelper_LoadState;
            this._navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private static async void CreateClockTask()
        {
            var result = await BackgroundExecutionManager.RequestAccessAsync();
            switch (result)
            {
                case BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity:
                case BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity:
                    await Task.Run(() => ClockTileScheduler.CreateSchedule());
                    EnsureUserPresentTask();
                    EnsureTimerTask();
                    break;
            }
        }

        private static void EnsureUserPresentTask()
        {
            if (BackgroundTaskRegistration.AllTasks.Any(task => task.Value.Name == TASKNAMEUSERPRESENT))
            {
                return;
            }

            var builder = new BackgroundTaskBuilder
            {
                Name = TASKNAMEUSERPRESENT,
                TaskEntryPoint = TASKENTRYPOINT
            };
            builder.SetTrigger(new SystemTrigger(SystemTriggerType.UserPresent, false));
            builder.Register();
        }

        private static void EnsureTimerTask()
        {
            if (BackgroundTaskRegistration.AllTasks.Any(task => task.Value.Name == TASKNAMETIMER))
            {
                return;
            }

            var builder = new BackgroundTaskBuilder
            {
                Name = TASKNAMETIMER,
                TaskEntryPoint = TASKENTRYPOINT
            };
            builder.SetTrigger(new TimeTrigger(180, false));
            builder.Register();
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var ds = DataContext as MainViewModel;
            if (ds != null)
            {
                await ds.LoadData(null);
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private async void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //var ds = this.DataContext as PageViewModel;
            //if (ds != null)
            //{
            //    e.PageState["CurrentSite"] = ds.CurrentSite.Id;
            //    e.PageState["SelectedIndex"] = PnlPivot.SelectedIndex;
            //    var result = await JsonConvert.SerializeObjectAsync(ServiceManager.Sites);
            //    await StringIOExtensions.WriteToFile(result, "data.txt");
            //}
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                var titleDetailsViewModel = DataContext as MainViewModel;
                if (titleDetailsViewModel != null)
                {
                    itemGridView.SelectedIndex = -1;
                }
            }

            CreateClockTask();

            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

    }
}