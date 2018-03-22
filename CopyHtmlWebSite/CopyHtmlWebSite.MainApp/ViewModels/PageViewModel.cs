namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Core.Extensions;
    using ViewModelBases;

    public class PageViewModel : ViewModelBase
    {
        private readonly CreateNewSiteUserControlViewModel _vm;
        public PageViewModel(CreateNewSiteUserControlViewModel vm)
        {
            _vm = vm;
        }
        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value, () =>
            {
                var page = _vm.GetPageByName(value);
                if (page != null)
                {
                    var tempPage = string.Empty;
                    var index = 1;
                    while (tempPage.HasNotText())
                    {
                        tempPage = $"{page.Name}_{index}";
                        if (_vm.GetPageByName(tempPage) != null)
                        {
                            index++;
                            tempPage = string.Empty;
                        }
                    }
                    _pageName = tempPage;
                    RaisePropertyChanged(nameof(PageName));
                }
                Onchanged();
            });
        }

        private string _pageSource;
        public string PageSource
        {
            get => _pageSource;
            set => SetProperty(ref _pageSource, value, Onchanged);
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
        }

        private void Onchanged()
        {
            _vm.AddNewPageCommand?.RaiseCanExecuteChanged();
            if (_vm.IsOpenAddSource)
            {
                _vm.AddPageWithSourceCommand.RaiseCanExecuteChanged();
            }
        }
    }
}