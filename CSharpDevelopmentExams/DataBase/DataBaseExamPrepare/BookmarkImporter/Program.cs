using System;
using System.Linq;
using System.Transactions;
using BookmarkImporter.Library;
using BookmarkImporter.Utils;
using DataBaseExamPrepare.Data;
using Bookmark = DataBaseExamPrepare.Data.Bookmark;

namespace BookmarkImporter
{
    class Program
    {
        private static void Main()
        {
            //XmlParser();
            //SimpleQuery();
            //ComplexQuery();
        }

        private static void ComplexQuery()
        {
            string inputFilePath = "../../complex-query.xml";
            var searchQuery = Helpers.DeserializeFromXml<ComplexQuery>(inputFilePath);

            SearchResults sr = new SearchResults();
            searchQuery.Query.ForEach(q =>
            {
                ResultSet rs = new ResultSet();
                var dbSearch = SessionState.db.Bookmarks.AsQueryable();
                if (q.Tag != null && q.Tag.Any())
                {
                    q.Tag.ForEach(t =>
                    {
                        dbSearch = dbSearch.Where(b => b.Tags.Any(x => x.Name == t));
                    });
                }

                if (!string.IsNullOrEmpty(q.Username))
                {
                    dbSearch = dbSearch.Where(b => b.User.Username == q.Username);
                }

                if (q.MaxResults > 0)
                {
                    dbSearch = dbSearch.Take(q.MaxResults);
                }
                else
                {
                    dbSearch = dbSearch.Take(10);
                }

                dbSearch.OrderBy(x => x.Title).ToList().ForEach(r =>
                {
                    Library.Bookmark b = new Library.Bookmark();
                    b.Username = r.User.Username;
                    b.Title = r.Title;
                    b.Url = r.URL;
                    b.Tags = string.Join(", ", r.Tags.OrderBy(x => x.Name).Select(x => x.Name));
                    b.Notes = r.Notes;
                    rs.Bookmarks.Add(b);
                });

                sr.ResultSet.Add(rs);
            });

            string outputFilePath = "../../search-results.xml";
            Helpers.SerializeToXml(sr, outputFilePath);
        }

        private static void SimpleQuery()
        {
            string filePath = "../../simple-query.xml";
            var searchQuery = Helpers.DeserializeFromXml<SearchQuery>(filePath);

            var dbSearch = SessionState.db.Bookmarks.Where(b => b.User.Username == searchQuery.Username);
            if (searchQuery.Tag != null)
            {
                dbSearch = dbSearch.Where(b => b.Tags.Any(t => t.Name == searchQuery.Tag));
            }
            
            var result = dbSearch.OrderBy(x => x.URL).ToList();
            Console.WriteLine(string.Join(Environment.NewLine, result.Select(x => x.URL)));
        }

        private static void XmlParser()
        {
            using (
                TransactionScope tran = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() {IsolationLevel = IsolationLevel.RepeatableRead}))
            {
                string filePath = "../../bookmarks.xml";
                var result = Helpers.DeserializeFromXml<Bookmarks>(filePath);
                result.bookmarks.ForEach(b =>
                {
                    AddBookmark(b.Username, b.Title, b.Url, b.Tags, b.Notes);
                });

                tran.Complete();
            }
        }

        private static void AddBookmark(string username, string title, string url, string tags, string notes)
        {
            var b = new Bookmark();

            var user = SessionState.db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                user = new User();
                user.Username = username;
                SessionState.db.Users.Add(user);
            }
            b.User = user;
            b.Title = title;
            //b.Title.Split(' ').ToList().ForEach(t =>
            //{
            //    ParseTags(t.Trim(), b);
            //});
            b.URL = url;

            //tags
            if (!string.IsNullOrEmpty(tags))
            {
                tags.Split(',').ToList().ForEach(t =>
                {
                    ParseTags(t.Trim(), b);
                });
            }

            b.Notes = notes;
            SessionState.db.Bookmarks.Add(b);
            SessionState.db.SaveChanges();
        }

        private static void ParseTags(string t, Bookmark b)
        {
            var tag = SessionState.db.Tags.FirstOrDefault(x => x.Name == t);
            if (tag == null)
            {
                tag = new Tag();
                tag.Name = t.ToLower();
                SessionState.db.Tags.Add(tag);
            }
            b.Tags.Add(tag);
            SessionState.db.SaveChanges();
        }
    }
}
