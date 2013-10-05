using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataSourceControls.WebData;

namespace DataSourceControls.Continents
{
    /// <summary>
    /// Summary description for ImageHttpHandler
    /// </summary>
    public class ImageHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString["Id"]))
            {
                int countryId = Convert.ToInt32(context.Request.QueryString["Id"]);
                if (countryId > 0)
                {
                    var result = RetrieveProductImage(countryId);
                    if (result != null)
                    {
                        context.Response.BinaryWrite(result);
                        context.Response.ContentType = "image/png";
                        context.Response.End();
                    }
                }
            }
        }


        private Byte[] RetrieveProductImage(int countryId)
        {
            ContinentsEntities db = new ContinentsEntities();
            var country = db.Countries.Find(countryId);
            if (country != null)
            {
                return country.Flag;
            }
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}