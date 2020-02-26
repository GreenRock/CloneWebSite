using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Models.Service;

namespace CopyHtmlWebSite.Core.Infrastructure
{
    using System.Collections.Generic;
    using Models;

    public interface IDataStorage
    {
        Task<ServiceResult<bool>> AddSite(SiteModel site);
        Task<ServiceResult<bool>> RemoveSite(SiteModel site);
        Task<ServiceResult<IList<SiteModel>>> GetSites();
    }
}