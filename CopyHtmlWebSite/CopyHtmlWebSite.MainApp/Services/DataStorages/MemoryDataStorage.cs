namespace CopyHtmlWebSite.MainApp.Services.DataStorages
{
    using System.Collections.Generic;
    using Models;

    public class MemoryDataStorage : IDataStorage
    {
        private static readonly IList<SiteModel> Sites = new List<SiteModel>();
        public bool AddSite(SiteModel site)
        {
            if (site == null)
                return false;
            Sites.Add(site);
            return true;
        }

        public bool RemoveSite(SiteModel site)
        {
            if (site == null)
                return false;
            Sites.Remove(site);
            return true;
        }

        public IList<SiteModel> GetSites()
        {
            return Sites;
        }
    }
}