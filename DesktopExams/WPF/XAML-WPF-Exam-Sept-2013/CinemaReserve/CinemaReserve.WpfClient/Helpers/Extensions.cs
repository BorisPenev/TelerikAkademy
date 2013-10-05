using System.Collections.ObjectModel;

namespace CinemaReserve.WpfClient.Helpers
{
    public static class Extensions
    {
        public static void TryRemove(this ObservableCollection<string> source, string value)
        {
            if (source.Contains(value))
                source.Remove(value);
        }
    }
}
