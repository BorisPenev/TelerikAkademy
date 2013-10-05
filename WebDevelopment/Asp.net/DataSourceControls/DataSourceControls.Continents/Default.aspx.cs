using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSourceControls.WebData;

namespace DataSourceControls.Continents
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSaveContinent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContinent.Text))
            {
                ContinentsEntities db = new ContinentsEntities();
                db.Continents.Add(new Continent()
                {
                    Name = Server.HtmlEncode(txtContinent.Text)
                });
                db.SaveChanges();
                lbContinents.DataBind();
            }
        }

        protected void btnSaveCountry_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCountryName.Text) &&
                !string.IsNullOrEmpty(txtCountryLanguage.Text) &&
                fuPicture.HasFile &&
                lbContinents.SelectedIndex >= 0)
            {
                Byte[] imgByte = null;
                HttpPostedFile File = fuPicture.PostedFile;
                imgByte = new Byte[File.ContentLength];
                File.InputStream.Read(imgByte, 0, File.ContentLength);

                var db = new ContinentsEntities();

                var c = new Country();
                if (btnSaveCountry.CommandArgument != null)
                {
                    c = db.Countries.Find(int.Parse(btnSaveCountry.CommandArgument));
                    btnSaveCountry.CommandArgument = null;
                }
                if (c == null)
                {
                    c = new Country();
                }

                c.Name = Server.HtmlEncode(txtCountryName.Text);
                c.Language = Server.HtmlEncode(txtCountryLanguage.Text);
                c.Flag = imgByte;
                c.ContinentId = int.Parse(lbContinents.SelectedValue);

                db.Countries.AddOrUpdate(c);
                db.SaveChanges();
                grdCountries.DataBind();
            }
        }

        protected void grdCountries_RowEditing(object sender, GridViewEditEventArgs e)
        {
            txtCountryName.Text = grdCountries.Rows[e.NewEditIndex].Cells[1].Text;
            txtCountryLanguage.Text = grdCountries.Rows[e.NewEditIndex].Cells[2].Text;
            btnSaveCountry.CommandArgument = grdCountries.DataKeys[e.NewEditIndex].Value.ToString();
            e.Cancel = true;
        }
    }
}