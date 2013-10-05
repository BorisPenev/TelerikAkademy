using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongodbDemo.Data;
using MongodbDemo.Data.Helpers;
using iTextSharp.text;
using MongodbDemo.Data.Library.Xml;
using Newtonsoft.Json;
using Book = MongodbDemo.Data.Library.Book;

namespace MongodbDemo.WebApp
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)//Main()
        {
            if (!IsPostBack)
            {
                grdresultFill();
            }
        }

        private void grdresultFill()
        {
            grdResult.DataSource = MongoDbProvider.db.LoadData<Book>().ToList();
            grdResult.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Title = txtBookTitle.Text;
            book.Author = txtBookAuthor.Text;
            book.PublishDate = DateTime.Now;

            MongoDbProvider.db.SaveData<Book>(book);
            grdresultFill();
        }

        protected void grdResult_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            MongoDbProvider.db.DeleteData<Book>(e.Keys[0].ToString());
            grdresultFill();
        }

        protected void grdResult_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            var id = new ObjectId(e.Keys[0].ToString());
            var book = MongoDbProvider.db.LoadData<Book>().FirstOrDefault(b => b._id == id);
            if (book != null)
            {
                book.Title = e.NewValues[0] == null ? string.Empty : e.NewValues[0].ToString();
                book.Author = e.NewValues[1] == null ? string.Empty : e.NewValues[1].ToString();
            }
            MongoDbProvider.db.SaveData(book);
            grdResult.EditIndex = -1;
            grdresultFill();
        }

        protected void grdResult_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            grdResult.EditIndex = e.NewEditIndex;
            grdresultFill();
        }

        protected void grdResult_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            grdResult.EditIndex = -1;
            grdresultFill();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdResult.DataSource = MongoDbProvider.db.LoadData<Book>().Where(b => b.Title.Contains(txtSearch.Text)).ToList();
            grdResult.DataBind();
        }


        protected void btnGeneratePdf_Click1(object sender, EventArgs e)
        {
            string str = string.Empty;
            using(StreamReader sr = new StreamReader(Server.MapPath("html.txt")))
            {
                str = sr.ReadToEnd();
            }

            StringBuilder sb = new StringBuilder();
            //sb.Append("<style>.th {background-color: red;}</style>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<th class=\"th\">Title</th>");
            sb.Append("<th>Author</th>");
            sb.Append("<th>PublishDate</th>");
            sb.Append("</tr>");
            MongoDbProvider.db.LoadData<Book>().ToList().ForEach(b =>
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", b.Title);
                sb.AppendFormat("<td>{0}</td>", b.Author);
                sb.AppendFormat("<td>{0}</td>", b.PublishDate);
                sb.Append("</tr>");
            });
            sb.Append("</table>");

            PDFBuilder.HtmlToPdfBuilder builder = new PDFBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            //builder.ImportStylesheet(AppDomain.CurrentDomain.BaseDirectory + "style.css"); 
            
            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(str);
            byte[] file = builder.RenderPdf();
            
            string tempFolder = AppDomain.CurrentDomain.BaseDirectory + "PdfResult\\";
            string tempFileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + ".pdf";
            if (Helpers.DirectoryExist(tempFolder))
            {
                if (!File.Exists(tempFolder + tempFileName))
                    File.WriteAllBytes(tempFolder + tempFileName, file);
            }

            
        }

        protected void btnGenerateJson_Click(object sender, EventArgs e)
        {
            List<Data.Library.Json.Book> bookList = new List<Data.Library.Json.Book>();

            MongoDbProvider.db.LoadData<Book>().ToList().ForEach(b =>
            {
                bookList.Add(new Data.Library.Json.Book()
                {
                    Author = b.Author,
                    PublishDate = DateTime.Parse(b.PublishDate.ToString()),
                    Title = b.Title
                });
            });

            string json = JsonConvert.SerializeObject(bookList, Formatting.Indented);
            Response.Write(json);

            List<Data.Library.Json.Book> jsonBooksList = JsonConvert.DeserializeObject<List<Data.Library.Json.Book>>(json);

            Books books = new Books();
            jsonBooksList.ForEach(j =>
            {
                books.BooklList.Add(new Data.Library.Xml.Book()
                {
                    Author = j.Author,
                    PublishDate = j.PublishDate.ToString("dd-MMM-yyyy"),
                    Title = j.Title
                });
            });

            string filePath = Server.MapPath("books.xml");
            Helpers.SerializeToXml(books, filePath);

            Data.Library.Xml.Books newBooks = new Data.Library.Xml.Books();
            newBooks = Helpers.DeserializeFromXml<Data.Library.Xml.Books>(filePath);
        }
    }
}

//<books>
//  <book title="dadas">
//     <author>fdafdsfds</author>
//  </book>
//</books>