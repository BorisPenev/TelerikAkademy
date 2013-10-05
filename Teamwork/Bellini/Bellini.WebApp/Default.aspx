<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bellini.WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblResultMessage" runat="server"></asp:Label><br />
        Upload excel report: <asp:FileUpload ID="txtFileUpload" runat="server" /> <asp:LinkButton ID="btnFileUpload" runat="server" OnClick="btnFileUpload_Click">File Upload</asp:LinkButton><br/><br/>
        <asp:LinkButton ID="btnGeneratePDFAggregatedSalesReports" runat="server" OnClick="btnGeneratePDFAggregatedSalesReports_Click">Generate PDF Aggregated Sales Reports</asp:LinkButton><br/><br/>
        <asp:LinkButton ID="btnGenerateXMLSalesReportbyVendors" runat="server" OnClick="btnGenerateXMLSalesReportbyVendors_Click">Generate XML Sales Report by Vendors</asp:LinkButton><br/><br/>
        <asp:LinkButton ID="btnGenerateJsonReportsAndSaveToMongoDb" runat="server" OnClick="btnGenerateJsonReportsAndSaveToMongoDb_Click">Generate Json Reports And Save To MongoDb</asp:LinkButton><br /><br/>
        Upload expenses xml: <asp:FileUpload ID="txtUploadExpenses" runat="server" /> <asp:LinkButton ID="btnUploadExpenses" runat="server" OnClick="btnUploadExpenses_Click">File Upload</asp:LinkButton><br/><br/>
        <asp:LinkButton ID="btnVendorsTotalReport" runat="server" OnClick="btnVendorsTotalReport_Click">Vendors Total Report</asp:LinkButton>
    </div>
    </form>
</body>
</html>
