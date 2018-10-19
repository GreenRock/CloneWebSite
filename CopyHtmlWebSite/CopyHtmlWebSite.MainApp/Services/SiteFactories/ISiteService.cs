namespace CopyHtmlWebSite.MainApp.Services.SiteFactories
{
    using System;
    using System.Threading.Tasks;
    using Core.Models;
    
    public interface ISiteService
    {
        Action<SiteModel> OnStart { set; get; }
        Action<SiteModel, string> OnError { set; get; }
        Action<SiteModel, FinishedSiteResult> OnFinish { set; get; }
        Action<SiteModel, SiteStatus> OnStatusChanged { set; get; }
        Task Run(SiteModel site);
    }
}