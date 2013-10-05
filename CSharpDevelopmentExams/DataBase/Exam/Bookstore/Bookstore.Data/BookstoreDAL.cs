using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Bookstore.Model.XmlLibrary;
using Author = Bookstore.Model.Author;
using Review = Bookstore.Model.Review;

namespace Bookstore.Data
{
    public static class BookstoreDAL
    {
        public static Model.Book CreateBookHeader(string title, long? isbn, double? price, string url)
        {
            var b = new Model.Book();

            b.Title = title;
            if (isbn.HasValue)
            {
                b.ISBN = isbn.Value;
            }

            if (price.HasValue)
            {
                b.Price = price.Value;
            }
            b.Url = url;

            return b;
        }

        public static bool AddBook(string title, long? isbn, string authors, double? price, string url)
        {
            var b = CreateBookHeader(title, isbn, price, url);
            b.Authors = GetAuthors(authors);
            SessionState.dbBookstore.Books.Add(b);
            try
            {
                SessionState.dbBookstore.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                SessionState.dbBookstore.Books.Remove(b);
                Console.WriteLine("Validation error. Title is too long or just missing for book with ISBN: " + isbn);
                return false;
            }
            return true;
        }

        public static bool AddBook(string title, long? isbn, Authors authors, double? price, string url, Reviews reviews)
        {
            var b = CreateBookHeader(title, isbn, price, url);
            b.Authors = GetAuthors(authors);
            b.Reviews = GetReviews(reviews);
            SessionState.dbBookstore.Books.Add(b);
            try
            {
                SessionState.dbBookstore.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                SessionState.dbBookstore.Books.Remove(b);
                Console.WriteLine("Validation error. Book Title or Author Name is too long or just missing for book with ISBN: " + isbn);
                return false;
            }
            return true;
        }

        public static ICollection<Model.Author> GetAuthors(string authorName)
        {
            var dbAuthor = GetAuthor(authorName);
            return new List<Model.Author>() { { dbAuthor } };
        }

        public static ICollection<Model.Author> GetAuthors(Authors author)
        {
            var result = new List<Model.Author>();

            if (author != null)
            {
                author.Author.ForEach(a =>
                {
                    var dbAuthor = GetAuthor(a.Name);
                    result.Add(dbAuthor);
                });
            }

            return result;
        }

        public static Model.Author GetAuthor(string authorName)
        {
            var dbAuthor = SessionState.dbBookstore.Authors.FirstOrDefault(x => x.Name == authorName);
            if (dbAuthor == null && !string.IsNullOrEmpty(authorName))
            {
                dbAuthor = new Author
                {
                    Name = authorName
                };
                SessionState.dbBookstore.Authors.Add(dbAuthor);
                try
                {
                    SessionState.dbBookstore.SaveChanges();
                }
                catch (DbEntityValidationException exception)
                {
                    SessionState.dbBookstore.Authors.Remove(dbAuthor);
                    Console.WriteLine("Validation error. Author Name is too long or just missing.");
                }
            }
            return dbAuthor;
        }

        public static ICollection<Model.Review> GetReviews(Reviews reviews)
        {
            var result = new List<Model.Review>();

            if (reviews != null)
            {
                reviews.reviews.ForEach(r =>
                {
                    var review = new Review();
                    review.Author = BookstoreDAL.GetAuthor(r.Author);

                    if (r.Text.StartsWith("\n"))
                    {
                        review.Text = r.Text.Remove(0, 2).Trim();
                    }
                    else
                    {
                        review.Text = r.Text;
                    }

                    if (r.CreatDate == null)
                    {
                        review.CreatDate = DateTime.Now;
                    }
                    else
                    {
                        review.CreatDate = DateTime.Parse(r.CreatDate);
                    }

                    result.Add(review);
                });
            }

            return result;
        }
    }
}
