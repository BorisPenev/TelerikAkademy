using System.Web.Mvc;

namespace Information.Areas.Offer
{
    public class OfferAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Offer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Offer_default",
                "Offer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}