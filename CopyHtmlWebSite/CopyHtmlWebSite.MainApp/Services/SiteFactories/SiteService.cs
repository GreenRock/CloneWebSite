namespace CopyHtmlWebSite.MainApp.Services.SiteFactories
{
    using System;
    using System.Threading.Tasks;
    using Core.Models;

    public class SiteService : ISiteService
    {
        public Action<SiteModel> OnStart { get; set; }
        public Action<SiteModel, string> OnError { get; set; }
        public Action<SiteModel, FinishedSiteResult> OnFinish { get; set; }
        public Action<SiteModel, SiteStatus> OnStatusChanged { get; set; }

        public async Task Run(SiteModel site)
        {
            void ChangeStatus(SiteStatus status)
            {
                OnStatusChanged?.Invoke(site, status);
            }

            try
            {
                OnStart(site);
                foreach (var page in site.Pages)
                {
                    using (var builder = new PageService(page, site))
                    {
                        ChangeStatus(SiteStatus.BeginLoadResource);
                        await builder.LoadResource();
                        ChangeStatus(SiteStatus.EndLoadResource);
                        builder.RemoveBlackList();
                        ChangeStatus(SiteStatus.RemoveBlackList);
                        builder.DownloadCss();
                        ChangeStatus(SiteStatus.DownloadCss);
                        builder.DownloadJs();
                        ChangeStatus(SiteStatus.DownloadJs);
                        builder.DownloadImage();
                        ChangeStatus(SiteStatus.DownloadImage);
                        builder.ReplaceCssInHtml();
                        ChangeStatus(SiteStatus.ReplaceCssInHtml);
                        builder.ReplaceJsInHtml();
                        ChangeStatus(SiteStatus.ReplaceJsInHtml);
                        builder.ReplaceImageInHtml();
                        ChangeStatus(SiteStatus.ReplaceImageInHtml);
                        builder.GetImageInCssFiles();
                        ChangeStatus(SiteStatus.GetImageInCssFile);
                        builder.DownloadImageInCssFile();
                        ChangeStatus(SiteStatus.DownloadImageInCssFile);
                        builder.ReplaceImageInCssFile();
                        ChangeStatus(SiteStatus.ReplaceImageInCssFile);
                        builder.PrepareToSave();
                        ChangeStatus(SiteStatus.PrepareToSave);
                        builder.SaveCss();
                        ChangeStatus(SiteStatus.SaveCss);
                        builder.SaveJs();
                        ChangeStatus(SiteStatus.SaveJs);
                        builder.SaveImage();
                        ChangeStatus(SiteStatus.SaveImage);
                        builder.SaveHtml();
                        ChangeStatus(SiteStatus.Done);
                    }
                }
                OnFinish(site, new FinishedSiteResult());
            }
            catch (Exception ex)
            {
                ChangeStatus(SiteStatus.Error);
                OnError?.Invoke(site, ex.ToString());
                OnFinish?.Invoke(site, null);
            }
        }
    }
}