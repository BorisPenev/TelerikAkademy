namespace EFPerformance.Data
{
    public static class SessionState
    {
        public static TelerikAcademyEntities _db = new TelerikAcademyEntities();
        public static TelerikAcademyEntities db
        {
            get
            {
                return _db;
            }
        }
    }
}
