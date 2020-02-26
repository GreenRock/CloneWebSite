using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;
using System;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
    public class PageViewModel : ViewModelBase
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value, () => PageNameChange?.Invoke(this));
        }

        private string _pageSource;
        public string PageSource
        {
            get => _pageSource;
            set => SetProperty(ref _pageSource, value);
        }

        private bool _isHtml;
        public bool IsHtml
        {
            get => _isHtml;
            set => SetProperty(ref _isHtml, value);
        }	

        public  void Reset()
        {
            PageName = PageSource = string.Empty;

            Id = string.Empty;

            IsHtml = false;
        }

        public Action<PageViewModel> PageNameChange;
    }
}