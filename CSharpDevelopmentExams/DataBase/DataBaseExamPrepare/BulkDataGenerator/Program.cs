using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataBaseExamPrepare.Data;
using DataBaseExamPrepare.Data.Extensions;

namespace BulkDataGenerator
{
    class Program
    {
        static void Main()
        {
            InsertUsers(30000);
            //InsertBookmarks(200000);
        }

        private static void InsertBookmarks(int count)
        {
            List<Bookmark> bookmarks = new List<Bookmark>();
            for (int i = 0; i < count; i++)
            {
                bookmarks.Add(new Bookmark()
                {
                    Title = "title" + i,
                    URL = "http://www.google.com/" + i,
                    UserId = 1
                });
            }

            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();

            // Do something
            SessionState.db.Database.BulkInsert("Bookmarks", bookmarks);

            // Stop timing
            stopwatch.Stop();

            // Write result
            Console.WriteLine("Time elapsed: {0}",
                stopwatch.Elapsed);
        }

        private static void InsertUsers(int count)
        {
            Random r = new Random();
            Stopwatch stopwatch = new Stopwatch();


            List<Tag> tags = new List<Tag>();
            for (int i = 0; i < 10000; i++)
            {
                tags.Add(new Tag()
                {
                    Name = "tag" + i
                });
            }

            stopwatch.Start();

            SessionState.db.Database.BulkInsert("Tags", tags);

            stopwatch.Stop();
            Console.WriteLine("10k Tags Inserted for: {0}", stopwatch.Elapsed);
            tags.Clear();
            tags.AddRange(SessionState.db.Tags.ToList());

            List<Bookmark> bookmarks = new List<Bookmark>();

            List<User> users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                User user = new User();
                user.Username = "user" + i;
                users.Add(user);
            }

            stopwatch.Start();

            SessionState.db.Database.BulkInsert("Users", users);

            stopwatch.Stop();
            Console.WriteLine("30k Users Inserted for: {0}", stopwatch.Elapsed);

            users.Clear();
            users.AddRange(SessionState.db.Users.ToList());
            
            users.ForEach(user =>
            {
                for (int i = 0; i < 7; i++)
                {
                    Bookmark bookmark = new Bookmark();
                    bookmark.Title = "title" + i;
                    bookmark.URL = "http://www.google.com/" + i;
                    bookmark.UserId = user.UserId;
                    
                    bookmarks.Add(bookmark);
                }
            });
            
            stopwatch.Start();

            SessionState.db.Database.BulkInsert("Bookmarks", bookmarks);

            stopwatch.Stop();
            Console.WriteLine("210k bookmarks Inserted for: {0}", stopwatch.Elapsed);

            bookmarks.Clear();
            bookmarks.AddRange(SessionState.db.Bookmarks.ToList());

            List<BookmarksInTags> bookmarksInTagses = new List<BookmarksInTags>();
            bookmarks.ForEach(bookmark =>
            {
                for (int k = 0; k < 10; k++)
                {
                    bookmarksInTagses.Add(new BookmarksInTags()
                    {
                        BookmarkId =  bookmark.BookmarkId,
                        TagId = tags[k].TagId
                    });
                } 
            });

            stopwatch.Start();

            SessionState.db.Database.BulkInsert("BookmarksInTags", bookmarksInTagses);

            stopwatch.Stop();
            Console.WriteLine("2100k bookmarksInTagses Inserted for: {0}", stopwatch.Elapsed);
        }
    }
}
