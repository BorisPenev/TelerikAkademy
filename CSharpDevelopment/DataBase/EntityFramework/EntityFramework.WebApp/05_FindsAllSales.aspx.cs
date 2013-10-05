using System;
using System.Data.Objects;
using System.Linq;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class FindsAllSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Write a method that finds all the sales by specified region and period (start / end dates).<br>");

            if (!IsPostBack)
            {
                ddlRegionFill();
            }
        }

        private void ddlRegionFill()
        {
            var regionList = SessionState.db.Orders.Where(o => o.ShipRegion != null).Select(o => o.ShipRegion).Distinct().ToList();
            ddlRegion.DataSource = regionList;
            ddlRegion.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var dbSearch =
                SessionState.db.Orders.Where(o =>
                    o.ShipRegion == ddlRegion.SelectedValue &&
                    o.ShippedDate.HasValue &&
                    (o.ShippedDate.Value >= txtStartDate.SelectedDate && o.ShippedDate.Value <= txtEndDate.SelectedDate )).ToList();

            grdResult.DataSource = dbSearch;
            grdResult.DataBind();
        }
    }
}