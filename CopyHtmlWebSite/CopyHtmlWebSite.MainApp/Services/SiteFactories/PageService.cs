using System;
using CopyHtmlWebSite.Core.Models;

namespace CopyHtmlWebSite.MainApp.Services.SiteFactories
{
    using System.Threading.Tasks;
    using HtmlAgilityPack;

    public class PageService : IDisposable
    {
        private PageModel _page;
        private SiteModel _site;
        private HtmlDocument _htmlSource;
        public PageService(PageModel page, SiteModel site)
        {
            this._page = page;
            this._site = site;
        }

        public Task LoadResource()
        {
            if (_page.IsHtml)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(_page.Source);
                _htmlSource = doc;
            }
            else
            {
                var web = new HtmlWeb();
                var doc = web.LoadFromBrowser(_page.Source);
                _htmlSource = doc;
            }
            return Task.CompletedTask;
        }

        public void RemoveBlackList()
        {
            throw new System.NotImplementedException();
        }

        public void DownloadCss()
        {
            throw new System.NotImplementedException();
        }

        public void DownloadJs()
        {
            throw new System.NotImplementedException();
        }

        public void DownloadImage()
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceCssInHtml()
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceJsInHtml()
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceImageInHtml()
        {
            throw new System.NotImplementedException();
        }

        public void DownloadImageInCssFile()
        {
            throw new System.NotImplementedException();
        }

        public void GetImageInCssFiles()
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceImageInCssFile()
        {
            throw new System.NotImplementedException();
        }

        public void SaveCss()
        {
            throw new System.NotImplementedException();
        }

        public void SaveJs()
        {
            throw new System.NotImplementedException();
        }

        public void SaveImage()
        {
            throw new System.NotImplementedException();
        }

        public void SaveHtml()
        {
            throw new System.NotImplementedException();
        }

        public void PrepareToSave()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}