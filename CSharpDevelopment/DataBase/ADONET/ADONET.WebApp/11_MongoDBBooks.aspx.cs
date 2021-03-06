﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using AdoNet.Data;
using AdoNet.Data.Library;
using AdoNet.Data.Helpers;
using iTextSharp.text;

namespace ADONET.WebApp
{
    public partial class MongoDbBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdResultFill();
            }
        }

        private void grdResultFill()
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

            MongoDbProvider.db.SaveData(book);
            grdResultFill();
        }

        protected void grdResult_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            MongoDbProvider.db.DeleteData<Book>(e.Keys[0].ToString());
            grdResultFill();
        }

        protected void grdResult_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            var book = MongoDbProvider.db.LoadData<Book>().FirstOrDefault(b => b._id.ToString() == e.Keys[0].ToString());
            if (book != null)
            {
                book.Title = e.NewValues[0] == null ? string.Empty : e.NewValues[0].ToString();
                book.Author = e.NewValues[1] == null ? string.Empty : e.NewValues[1].ToString();
            }
            MongoDbProvider.db.SaveData(book);
        }

        protected void grdResult_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            grdResult.EditIndex = e.NewEditIndex;
            grdResultFill();
        }

        protected void grdResult_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            grdResult.EditIndex = -1;
            grdResultFill();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                grdResultFill();
            }
            else
            {
                grdResult.DataSource =
                MongoDbProvider.db.LoadData<Book>().Where(b => b.Title.Contains(txtSearch.Text)).ToList();
                grdResult.DataBind();
            }
        }

        protected void btnGeneratePdf_Click1(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<th>Title</th>");
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
            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(sb.ToString());
            byte[] file = builder.RenderPdf();
            string tempFolder = AppDomain.CurrentDomain.BaseDirectory + "PdfResult\\";
            string tempFileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + ".pdf";
            if (Helpers.DirectoryExist(tempFolder))
            {
                if (!File.Exists(tempFolder + tempFileName))
                    File.WriteAllBytes(tempFolder + tempFileName, file);
            }
        }
    }
}