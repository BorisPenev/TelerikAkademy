using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using Bookstore.Data;
using Bookstore.Model;
using Bookstore.Model.Utils;
using Bookstore.Model.XmlLibrary.Results;
using Bookstore.Model.XmlLibrary.Search;
using Book = Bookstore.Model.XmlLibrary.Search.SampleBook;

namespace Bookstore.Client
{
    internal class Program
    {
        private static void Main()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BookstoreContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookstoreContext,
                Data.Migrations.Configuration>());

            var config = new Data.Migrations.Configuration();
            var migrator = new DbMigrator(config);
            migrator.Update();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //ImportSimpleBooks();

            //ImportComplexBooks();

            //SimpleQuery("../../xml/test6.xml");

            ////SimpleQuery("../../reviews-queries.xml");

            SearchReviews();
        }

        private static void ImportSimpleBooks()
        {
            #region parseXml

            string filePath = "../../xml/simple-books.xml";
            var result = Helpers.DeserializeFromXml<SampleCatalog>(filePath);

            #endregion

            result.Books.ForEachStoppable(b => BookstoreDAL.AddBook(b.Title, b.ISBN, b.Author, b.Price, b.Url));
        }

        private static void ImportComplexBooks()
        {
            #region parseXml

            string filePath = "../../xml/complex-books.xml";
            var result = Helpers.DeserializeFromXml<ComplexCatalog>(filePath);

            #endregion

            result.Books.ForEachStoppable(b => BookstoreDAL.AddBook(b.Title, b.ISBN, b.Authors, b.Price, b.Url, b.Reviews));
        }

        private static void SimpleQuery(string filePath)
        {
            var result = Helpers.DeserializeFromXml<SearchQuery>(filePath);

            var dbSearch = SessionState.dbBookstore.Books.AsQueryable();
            if (!string.IsNullOrEmpty(result.Title))
            {
                dbSearch = dbSearch.Where(d => d.Title.ToLower() == result.Title.ToLower());
            }

            if (!string.IsNullOrEmpty(result.Author))
            {
                dbSearch = dbSearch.Where(b => b.Authors.Any(a => a.Name.ToLower() == result.Author.ToLower()));
            }

            if (result.ISBN != null)
            {
                dbSearch = dbSearch.Where(b => b.ISBN == result.ISBN);
            }

            var dbResult = dbSearch.OrderBy(b => b.Title).ToList();
            if (dbResult.Any())
            {
                Console.WriteLine("{0} books found:", dbResult.Count());
                dbResult.ForEach(b =>
                {
                    if (b.Reviews.Count > 0)
                    {
                        Console.WriteLine("{0} --> {1}", b.Title, b.Reviews.Count);
                    }
                    else
                    {
                        Console.WriteLine("{0} --> {1} reviews", b.Title, "no");
                    }
                });
            }
            else
            {
                Console.WriteLine("Nothing found");
            }
        }

        private static void SearchReviews()
        {
            string filePath = "../../xml/test4.xml";
            var result = Helpers.DeserializeFromXml<ReviewQueries>(filePath);

            SearchResult sr = new SearchResult();

            result.ReviewQueryList.ForEach(query =>
            {
                ResultSet rs = new ResultSet();
                var dbSearch = SessionState.dbBookstore.Reviews.AsQueryable();
                switch (query.Type)
                {
                    case "by-period":
                        var startDate = DateTime.Parse(query.StartDate);
                        var endDate = DateTime.Parse(query.EndDate);
                        dbSearch = dbSearch.Where(r => r.CreatDate >= startDate && r.CreatDate <= endDate);
                        break;
                    default:
                        dbSearch =
                            dbSearch.Include("Author")
                                .Where(r => r.Author.Name == query.Author)
                                .OrderBy(o => o.CreatDate)
                                .ThenBy(o => o.Text);
                        break;
                }

                var res = dbSearch.Include("Book").ToList();

                res.ForEach(r =>
                {
                    ResultReview rr = new ResultReview();
                    rr.CreatDate = r.CreatDate.ToString("dd-MMM-yyyy");
                    rr.Text = r.Text;
                    rr.ResultBook = new ResultBook
                    {
                        Title = r.Book.Title,
                        Authors = string.Join(", ", r.Book.Authors.Select(a => a.Name)),
                        ISBN = r.Book.ISBN,
                        Url = r.Book.Url
                    };
                    rs.ResultReviews.Add(rr);
                });

                sr.ResultSets.Add(rs);

                //logs
                //SessionState.dbLogs.SearchLogs.Add(new SearchLogs()
                //{
                //    Date = DateTime.Now,
                //    QueryXml = Helpers.SerializeToXmlToString(query)
                //});
                //SessionState.dbLogs.SaveChanges();
            });

            string outputFilePath = "../../reviews-search-results.xml";
            Helpers.SerializeToXml(sr, outputFilePath);
        }
    }
}
