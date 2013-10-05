namespace HiddenTruth.Library.Services.Navigation
{
    public class NavigationMessage : InitMessageBase
    {
        public string SiteId { get; set; }

        public string PageToken { get; set; }

        public NavigationMessage()
        {

        }

        public NavigationMessage(string siteId, string pageToken)
        {
            this.SiteId = siteId;
            this.PageToken = pageToken;
        }
    }
}
