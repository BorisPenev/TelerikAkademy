using System;
using System.IO;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Dictionary.Data;
using Dictionary.Data.Library;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;

namespace Dictionary.WebApp
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
            grdResult.DataSource = MongoDbProvider.db.LoadData<Word>().ToList();
            grdResult.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Word word = new Word
            {
                WordName = textWordName.Text,
                Translation = textWordTranslation.Text
            };

            MongoDbProvider.db.SaveData<Word>(word);
            grdresultFill();
        }

        protected void grdResult_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            MongoDbProvider.db.DeleteData<Word>(e.Keys[0].ToString());
            grdresultFill();
        }

        protected void grdResult_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            var id = new ObjectId(e.Keys[0].ToString());
            var word = MongoDbProvider.db.LoadData<Word>().FirstOrDefault(b => b._id == id);
            if (word != null)
            {
                word.WordName = e.NewValues[0] == null ? string.Empty : e.NewValues[0].ToString();
                word.Translation = e.NewValues[1] == null ? string.Empty : e.NewValues[1].ToString();
            }
            MongoDbProvider.db.SaveData(word);
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
            grdResult.DataSource =
                MongoDbProvider.db.LoadData<Word>().Where(b => b.WordName.Contains(txtSearch.Text)).ToList();
            grdResult.DataBind();
        }

        protected void btnGeneratePdf_Click1(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<th class=\"th\">Title</th>");
            sb.Append("<th>Author</th>");
            sb.Append("</tr>");
            MongoDbProvider.db.LoadData<Word>().ToList().ForEach(b =>
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", b.WordName);
                sb.AppendFormat("<td>{0}</td>", b.Translation);
                sb.Append("</tr>");
            });
            sb.Append("</table>");

            using (var document = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(document,
                    new FileStream(Server.MapPath("loremipsum5.pdf"), FileMode.Append));
                document.Open();

                var htmlContext = new HtmlPipelineContext(null);
                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                //change this to your CCS file location
                cssResolver.AddCssFile(Server.MapPath("style.css"), true);
                var pipeline = new CssResolverPipeline(cssResolver,
                    new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
                var worker = new XMLWorker(pipeline, true);
                var p = new XMLParser(worker);
                p.Parse(new StringReader(sb.ToString()));
            }
        }
    }
}