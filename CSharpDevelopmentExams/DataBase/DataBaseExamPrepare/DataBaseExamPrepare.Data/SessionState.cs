namespace DataBaseExamPrepare.Data
{
    public static class SessionState
    {
        public static BookmarksEntities _db = new BookmarksEntities();
        public static BookmarksEntities db
        {
            get
            {
                return _db;
            }
        }
    }
}
