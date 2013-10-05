using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Binding
{
    public partial class SearchCar : System.Web.UI.Page
    {
        List<Producer> producers = new List<Producer>();
        List<Extra> extras = new List<Extra>();

        protected void Page_Init(object sender, EventArgs e)
        {
            producers.Add(new Producer()
            {
                Id = 1,
                Name = "BMW",
                Models = new List<Model>()
                {
                    new Model()
                    {
                        Id = 1,
                        Name = "114"
                    },
                    new Model()
                    {
                        Id = 2,
                        Name = "1602"
                    },
                    new Model()
                    {
                        Id = 3,
                        Name = "725"
                    }
                }
            });
            producers.Add(new Producer()
            {
                Id = 2,
                Name = "Bentley",
                Models = new List<Model>()
                {
                    new Model()
                    {
                        Id = 4,
                        Name = "Arnage"
                    },
                    new Model()
                    {
                        Id = 5,
                        Name = "Continental"
                    },
                    new Model()
                    {
                        Id = 6,
                        Name = "Mulsanne"
                    }
                }
            });
            producers.Add(new Producer()
            {
                Id = 3,
                Name = "Cadillac",
                Models = new List<Model>()
                {
                    new Model()
                    {
                        Id = 7,
                        Name = "Allante"
                    },
                    new Model()
                    {
                        Id = 8,
                        Name = "Deville"
                    },
                    new Model()
                    {
                        Id = 9,
                        Name = "Fleetwood"
                    }
                }
            });

            extras.Add(new Extra()
            {
                Id = 1,
                Name = "Vertikalno izlitane"
            });
            extras.Add(new Extra()
            {
                Id = 2,
                Name = "extra 2"
            });
            extras.Add(new Extra()
            {
                Id = 3,
                Name = "Extra 3"
            });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlProducers.DataSource = producers;
                ddlProducers.DataValueField = "Id";
                ddlProducers.DataTextField = "Name";
                ddlProducers.DataBind();
                ddlProducers_SelectedIndexChanged(ddlProducers, null);

                cbExtras.DataSource = extras;
                cbExtras.DataValueField = "Id";
                cbExtras.DataTextField = "Name";
                cbExtras.DataBind();
            }
        }

        protected void ddlProducers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(ddlProducers.SelectedValue.ToString());
            ddlModels.DataSource = producers.FirstOrDefault(p => p.Id == id).Models;
            ddlModels.DataValueField = "Id";
            ddlModels.DataTextField = "Name";
            ddlModels.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblResult.Text = string.Format("Producer: {0}, Model: {1}, Extras: {2}, Engine: {3}",
                ddlProducers.SelectedItem.Text,
                ddlModels.SelectedItem.Text,
                string.Join(", ", cbExtras.Items.Cast<ListItem>().Where(x => x.Selected).ToList()),
                rbEngine.SelectedItem.Text); ;
        }
    }
}