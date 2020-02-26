namespace CopyHtmlWebSite.Core.Models
{
    using System.Collections.Generic;

    public class SiteModel
    {
        public SiteModel() : this(SiteStatus.Pending)
        {
            
        }

        public SiteModel(SiteStatus status)
        {
            Status = status;
        }

        public string SiteName { get; set; }
        public string RootSite { get; set; }
        public IEnumerable<PageModel> Pages { get; set; }
        public SiteStatus Status { get; set; }
    }
}