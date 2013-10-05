namespace EntityFramework.Data
{
    public static class SessionState
    {
        public static NORTHWNDEntities _db = new NORTHWNDEntities();
        public static NORTHWNDEntities db
        {
            get
            {
                return _db;
            }
        }
    }
}
