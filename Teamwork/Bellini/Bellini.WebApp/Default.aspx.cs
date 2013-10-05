using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Bellini.Data;
using Bellini.Data.Library;
using Bellini.Data.Readers;
using Bellini.Data.ReportGenerators;
using Bellini.Library;

namespace Bellini.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            if (this.txtFileUpload.HasFile)
            {
                string uploadFolder = Server.MapPath("/Upload/");
                string filePath = uploadFolder + this.txtFileUpload.FileName;
                if (Helpers.DirectoryExist(uploadFolder))
                {
                    this.txtFileUpload.SaveAs(filePath);
                }

                string outputFolder = Server.MapPath("/Unzipped/" + Guid.NewGuid());
                string moveFolder = Server.MapPath("/Archive/" + Guid.NewGuid());
                if (Helpers.DirectoryExist(uploadFolder) && Helpers.DirectoryExist(moveFolder))
                {
                    ZipUtil.UnZipFiles(filePath, outputFolder, null, false, moveFolder);
                }

                var reports = ExcelReader.GetSupermarketReportsByDate(outputFolder);

                Directory.Delete(outputFolder, true);

                lblResultMessage.Text = "Upload file completed<br />" +
                                        "Unzip file completed<br />" +
                                        "Read excel reports and import to mssql completed";
                lblResultMessage.ForeColor = Color.Green;
            }
        }

        protected void btnGeneratePDFAggregatedSalesReports_Click(object sender, EventArgs e)
        {
            string folderReport = Server.MapPath("/Reports/Pdf/");
            string fileName = Guid.NewGuid() + ".pdf";
            string filePath = folderReport + fileName;
            if (Helpers.DirectoryExist(folderReport))
            {
                PdfReportGenerator.GeneratePdfReport(filePath);
                if (File.Exists(filePath))
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "Application/pdf";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                    response.TransmitFile(filePath);
                    response.Flush();
                    response.End();
                }
            }
        }

        protected void btnGenerateXMLSalesReportbyVendors_Click(object sender, EventArgs e)
        {
            string folderReport = Server.MapPath("/Reports/Xml/");
            string fileName = Guid.NewGuid() + ".xml";
            string filePath = folderReport + fileName;
            if (Helpers.DirectoryExist(folderReport))
            {
                XmlReportGenerator.GenerateXmlReport(filePath);
                if (File.Exists(filePath))
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "Application/xml";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                    response.TransmitFile(filePath);
                    response.Flush();
                    response.End();
                }
            }
        }

        protected void btnGenerateJsonReportsAndSaveToMongoDb_Click(object sender, EventArgs e)
        {
            string folderReport = Server.MapPath("/Reports/Json/" + Guid.NewGuid() +"/");
            if (Helpers.DirectoryExist(folderReport))
            {
                JsonReportGenerator.GenerateJsonReports(folderReport);
            }
            
            string downloadFolder = Server.MapPath("/Downloads/");
            string fileName = Guid.NewGuid() + ".zip";
            string filePath = downloadFolder + fileName;
            if (Helpers.DirectoryExist(downloadFolder))
            {
                ZipUtil.ZipFiles(folderReport, filePath, null);
                if (File.Exists(filePath))
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "Application/zip";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                    response.TransmitFile(filePath);
                    response.Flush();
                    response.End();
                }
            }
        }

        protected void btnUploadExpenses_Click(object sender, EventArgs e)
        {
            if (this.txtUploadExpenses.HasFile)
            {
                MongoDbProvider.db.DropCollection("VendorExpenseMongoDB");

                var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)SessionState.db).ObjectContext;
                objCtx.ExecuteStoreCommand("DELETE FROM Expenses");
                objCtx.ExecuteStoreCommand("DELETE FROM VendorExpenseSqls");
                SessionState.db.SaveChanges();

                string uploadFolder = Server.MapPath("/Upload/");
                string filePath = uploadFolder + this.txtUploadExpenses.FileName;
                if (Helpers.DirectoryExist(uploadFolder))
                {
                    this.txtUploadExpenses.SaveAs(filePath);

                    var vendorExpenses = VendorExpenseReader.Read(filePath);

                    vendorExpenses.ForEach(v =>
                    {
                        var vendorMongoDB = new VendorExpenseMongoDB();
                        vendorMongoDB.VendorName = v.VendorName;
                        vendorMongoDB.Expenses = v.Expenses;
                        MongoDbProvider.db.SaveData(vendorMongoDB);
                    });

                    foreach (var vendorExpense in vendorExpenses)
                    {
                        SessionState.db.VendorExpenses.Add(vendorExpense);
                    }
                    SessionState.db.SaveChanges();

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    lblResultMessage.Text = "Expenses report imported<br />";
                    lblResultMessage.ForeColor = Color.Green;
                }
            }
        }

        protected void btnVendorsTotalReport_Click(object sender, EventArgs e)
        {
            string folderReport = Server.MapPath("/Reports/excel/");
            string fileName = Guid.NewGuid() + "Products-Total-Report.xlsx";
            string filePath = folderReport + fileName;
            if (Helpers.DirectoryExist(folderReport))
            {
                ExcelReportGenerator.GenerateExcelReport(filePath);
                if (File.Exists(filePath))
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "Application/xlsx";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                    response.TransmitFile(filePath);
                    response.Flush();
                    response.End();
                }
            }
        }
    }
}