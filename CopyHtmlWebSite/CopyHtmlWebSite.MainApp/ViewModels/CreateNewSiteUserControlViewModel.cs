namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;
    using Core.Extensions;
    using Core.Infrastructure;
    using Models;
    using Prism.Commands;
    using Prism.Regions;
    using Properties;
    using Services.DataStorages;
    using ViewModelBases;
    using Views;

    public class CreateNewSiteUserControlViewModel : NavigationViewModelBase
    {
        private readonly IDataStorage _dataStorage;
        private readonly IConverter _converter;
        public CreateNewSiteUserControlViewModel(
            IRegionManager regionManager,
            IDataStorage dataStorage,
            IConverter converter)
            : base(regionManager)
        {
            _dataStorage = dataStorage;
            _converter = converter;
            Pages = new ObservableCollection<PageModel>();
            Pages.CollectionChanged -= PagesOnCollectionChanged;
            Pages.CollectionChanged += PagesOnCollectionChanged;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Init();
        }

        private void Init()
        {
            Pages.Clear();
            Page = new PageViewModel(this);
            SiteName = string.Empty;
        }

        private void PagesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            AddSiteCommand.RaiseCanExecuteChanged();
        }

        private bool _isOpenAddSource;
        public bool IsOpenAddSource
        {
            get => _isOpenAddSource;
            set => SetProperty(ref _isOpenAddSource, value);
        }

        private ObservableCollection<PageModel> _pages;
        public ObservableCollection<PageModel> Pages
        {
            get => _pages;
            set => SetProperty(ref _pages, value);
        }

        private string _siteName = string.Empty;
        public string SiteName
        {
            get => _siteName;
            set => SetProperty(ref _siteName, value);
        }

        private PageViewModel _page;
        public PageViewModel Page
        {
            get => _page;
            set => SetProperty(ref _page, value);
        }

        private DelegateCommand _addSiteCommand;
        public DelegateCommand AddSiteCommand => _addSiteCommand ?? (_addSiteCommand =
                                              new DelegateCommand(ExecuteAddSiteCommand, 
                                                      () => !IsBusy 
                                                            && SiteName.HasText() 
                                                            && Pages.Any())
                                                  .ObservesProperty(() => IsBusy)
                                                  .ObservesProperty(() => SiteName));

        private void ExecuteAddSiteCommand()
        {
            _dataStorage.AddSite(new SiteModel
            {
                SiteName = SiteName.Trim(),
                Pages = Pages.ToList(),
                Status = SiteStatus.Pending
            });
            NavigateTo(nameof(MainUserControl));
        }

        private DelegateCommand _addNewPageCommand;

        public DelegateCommand AddNewPageCommand => _addNewPageCommand ?? (_addNewPageCommand =
                                        new DelegateCommand(ExecuteAddNewPageCommand,
                                                () => !IsBusy 
                                                      && Page != null 
                                                      && Page.PageSource.HasText() 
                                                      && Regex.IsMatch(Page.PageSource, RegexResource.Url))
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => Page));

        private void ExecuteAddNewPageCommand()
        {
            var pageModel = _converter.Map<PageViewModel, PageModel>(Page);
            pageModel.IsHtml = false;
            AddAPage(pageModel);
        }

        private DelegateCommand _addPageWithSourceCommand;
        public DelegateCommand AddPageWithSourceCommand => _addPageWithSourceCommand ?? (_addPageWithSourceCommand =
                                        new DelegateCommand(ExecuteAddPageWithSourceCommand, 
                                                () => !IsBusy 
                                                      && Page != null 
                                                      && Page.PageName.HasText() 
                                                      && Page.PageSource.HasText())
                                                .ObservesProperty(() => IsBusy)
                                                .ObservesProperty(() => Page));

        private void ExecuteAddPageWithSourceCommand()
        {
            var pageModel = _converter.Map<PageViewModel, PageModel>(Page);
            pageModel.IsHtml = true;
            AddAPage(pageModel);
        }

        private ICommand _showPageSourceCommand;
        public ICommand ShowPageSourceCommand => _showPageSourceCommand ?? (_showPageSourceCommand =
                                        new DelegateCommand(() => IsOpenAddSource = !IsOpenAddSource, () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private ICommand _removePageCommand;

        public ICommand RemovePageCommand => _removePageCommand ?? (_removePageCommand =
                                        new DelegateCommand<PageModel>(ExecuteRemovePageCommand, page => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteRemovePageCommand(PageModel page)
        {
            if (page != null)
            {
                Pages.Remove(page);
            }
        }

        internal PageModel GetPageByName(string name)
        {
            return Pages.FirstOrDefault(x => x.Name.Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        private void AddAPage(PageModel pageModel)
        {
            Pages.Add(pageModel);
            Page.Reset();
            AddSiteCommand.RaiseCanExecuteChanged();
        }
    }
}
