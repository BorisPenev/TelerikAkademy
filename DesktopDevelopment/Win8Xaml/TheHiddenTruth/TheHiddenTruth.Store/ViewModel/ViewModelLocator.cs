/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Mvvm.Store"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;
using TheHiddenTruth.Library.ViewModel;
using TheHiddenTruth.Store.Services;
using TheHiddenTruth.Store.View;
using Microsoft.Practices.ServiceLocation;

namespace TheHiddenTruth.Store.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (!SimpleIoc.Default.IsRegistered<IServiceManager>())
            {
                // For use the fake data do:
                // FakeServiceManager.FakeImagePath = "ms-appx:///Images/FakeImage.png";
                // SimpleIoc.Default.Register<IServiceManager, FakeServiceManager>();
                SimpleIoc.Default.Register<IServiceManager, ServiceManager>();

                SimpleIoc.Default.Register<INavigationService>(() => new NavigationService());
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PageViewModel>();
            SimpleIoc.Default.Register<ItemViewModel>();
            SimpleIoc.Default.Register<PivotItemViewModel>();
            SimpleIoc.Default.Register<SearchResultViewModel>();
            SimpleIoc.Default.Register<CustomAppBarViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public PageViewModel Page
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PageViewModel>();
            }
        }

        public ItemViewModel Item
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ItemViewModel>();
            }
        }

        public PivotItemViewModel PivotItem
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PivotItemViewModel>();
            }
        }

        public SearchResultViewModel SearchResult
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchResultViewModel>();
            }
        }

        public CustomAppBarViewModel CustomAppBar
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CustomAppBarViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}