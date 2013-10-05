using CinemaReserve.ResponseModels;

namespace CinemaReserve.WpfClient.Config
{
    public static class AppCache
    {
        public static Configuration Config { get; private set; }

        static AppCache()
        {
            Config = new Configuration();
        }
    }
}
