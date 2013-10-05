using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CinemaReserve.ResponseModels;
using CinemaReserve.WpfClient.Views;

namespace CinemaReserve.WpfClient.Behavior
{
    public static class EventToCommandBehavior
    {
        public static readonly DependencyProperty SearchCommand =
            EventBehaviourFactory.CreateCommandExecutionEventBehaviour(TextBox.KeyUpEvent, "SearchCommand", typeof(EventToCommandBehavior));

        public static void SetSearchCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(SearchCommand, value);
        }

        public static ICommand GetSearchCommand(DependencyObject o)
        {
            return o.GetValue(SearchCommand) as ICommand;
        }

        public static readonly DependencyProperty SelectedCommand =
            EventBehaviourFactory.CreateCommandExecutionEventBehaviour(ListBox.SelectionChangedEvent, "SelectedCommand", typeof(EventToCommandBehavior));

        public static void SetSelectedCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(SelectedCommand, value);
        }

        public static ICommand GetSelectedCommand(DependencyObject o)
        {
            return o.GetValue(SelectedCommand) as ICommand;
        }

        public static readonly DependencyProperty LoadedCommand =
            EventBehaviourFactory.CreateCommandExecutionEventBehaviour(UserControl.LoadedEvent, "LoadedCommand", typeof(EventToCommandBehavior));

        public static void SetLoadedCommand(DependencyObject o, ICommand value)
        {
            o.SetValue(LoadedCommand, value);
        }

        public static ICommand GetLoadedCommand(DependencyObject o)
        {
            return o.GetValue(LoadedCommand) as ICommand;
        }
    }
}
