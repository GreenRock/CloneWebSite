namespace CopyHtmlWebSite.Core.Infrastructure
{
    using System.Collections.Generic;
    using Models;

    public interface IDataStorage
    {
        bool AddSite(SiteModel site);
        bool RemoveSite(SiteModel site);
        IList<SiteModel> GetSites();
    }
}