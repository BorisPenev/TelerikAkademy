namespace Bellini.Data
{
    public static class SessionState
    {
        public static SupermarketSQLDbContext _db = new SupermarketSQLDbContext();
        public static SupermarketSQLDbContext db
        {
            get
            {
                return _db;
            }
        }
    }
}
