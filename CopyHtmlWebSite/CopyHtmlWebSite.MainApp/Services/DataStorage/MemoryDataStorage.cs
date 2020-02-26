using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.Core.Infrastructure;
using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.Core.Models.Service;
using CopyHtmlWebSite.MainApp.Properties;

namespace CopyHtmlWebSite.MainApp.Services.DataStorage
{
    public class MemoryDataStorage : IDataStorage
    {
        private static readonly IList<SiteModel> Sites = new List<SiteModel>();

        public Task<ServiceResult<bool>> AddSite(SiteModel site)
        {
            var serviceResult = new ServiceResult<bool>();
            if (site == null)
                return Task.FromResult(serviceResult);

            site.SiteName = Regex.Replace(site.SiteName, RegexResource.SpecialChar, string.Empty);

            var siteRootSite = site.RootSite;

            if (siteRootSite.HasText())
            {
                var pattern = string.Join("|", RegexResource.Space, RegexResource.UrlIntoQuotation, RegexResource.LastSlashCharacter);
                site.RootSite = Regex.Replace(siteRootSite, pattern, string.Empty);
            }

            Sites.Add(site);

            return Task.FromResult(serviceResult.Completed(true));
        }

        public Task<ServiceResult<bool>> RemoveSite(SiteModel site)
        {
            var serviceResult = new ServiceResult<bool>();
            if (site == null)
                return Task.FromResult(serviceResult);
            Sites.Remove(site);
            return Task.FromResult(serviceResult.Completed(true));
        }

        public Task<ServiceResult<IList<SiteModel>>> GetSites()
        {
            var serviceResult = new ServiceResult<IList<SiteModel>>();
            return Task.FromResult(serviceResult.Completed(Sites));
        }
    }
}