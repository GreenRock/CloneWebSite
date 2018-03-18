namespace CopyHtmlWebSite.MainApp.Models
{
    using System.Collections.Generic;

    public class SiteModel
    {
        public string SiteName { get; set; }
        public List<PageModel> Pages { get; set; }
        public SiteStatus Status { get; set; }
    }
}