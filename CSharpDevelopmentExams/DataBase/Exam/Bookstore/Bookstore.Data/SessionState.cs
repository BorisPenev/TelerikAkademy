namespace Bookstore.Data
{
    public static class SessionState
    {
        public static BookstoreContext _dbBookstore = new BookstoreContext();
        public static BookstoreContext dbBookstore
        {
            get
            {
                return _dbBookstore;
            }
        }

        public static LogsContext _dbLogs = new LogsContext();
        public static LogsContext dbLogs
        {
            get
            {
                return _dbLogs;
            }
        }
    }
}
