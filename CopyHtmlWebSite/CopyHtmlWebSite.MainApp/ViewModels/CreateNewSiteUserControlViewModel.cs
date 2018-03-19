namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Core.Extensions;
    using Models;
    using Prism.Commands;
    using Prism.Regions;
    using Services.DataStorages;
    using ViewModelBases;
    using Views;

    public class CreateNewSiteUserControlViewModel : NavigationViewModelBase
    {
        private readonly IDataStorage _dataStorage;
        public CreateNewSiteUserControlViewModel(
            IRegionManager regionManager,
            IDataStorage dataStorage)
            : base(regionManager)
        {
            _dataStorage = dataStorage;
            Pages = new ObservableCollection<PageModel>();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            SiteName = PageName = PageLink = string.Empty;
            Pages.Clear();
        }

        private ObservableCollection<PageModel> _pages;
        public ObservableCollection<PageModel> Pages
        {
            get => _pages;
            set => SetProperty(ref _pages, value);
        }

        private string _siteName;
        public string SiteName
        {
            get => _siteName;
            set => SetProperty(ref _siteName, value);
        }

        private string _pageName;

        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private string _pageLink;

        public string PageLink
        {
            get => _pageLink;
            set => SetProperty(ref _pageLink, value);
        }

        private PageModel _pageSelected;
        public PageModel PageSelected
        {
            get => _pageSelected;
            set => SetProperty(ref _pageSelected, value);
        }

        private ICommand _addSiteCommand;
        public ICommand AddSiteCommand => _addSiteCommand ?? (_addSiteCommand =
                                              new DelegateCommand(ExecuteAddSiteCommand, () => !IsBusy && SiteName.HasText() && Pages.Any())
                                                  .ObservesProperty(() => IsBusy)
                                                  .ObservesProperty(() => Pages)
                                                  .ObservesProperty(() => SiteName));

        private void ExecuteAddSiteCommand()
        {
            _dataStorage.AddSite(new SiteModel
            {
                SiteName = SiteName,
                Pages = Pages.ToList(),
                Status = SiteStatus.Pending
            });
            NavigateTo(nameof(MainUserControl));
        }

        private ICommand _addNewPageCommand;

        public ICommand AddNewPageCommand => _addNewPageCommand ?? (_addNewPageCommand =
                                        new DelegateCommand(ExecuteAddNewPageCommand,
                                                () => !IsBusy && PageName.HasText() && PageLink.HasText())
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => PageName)
                                            .ObservesProperty(() => PageLink));

        private void ExecuteAddNewPageCommand()
        {
            var page = GetPageByName(PageName);
            if (page == null)
            {
                Pages.Add(new PageModel
                {
                    Name = PageName,
                    Link = PageLink
                });
                PageName = PageLink = string.Empty;
            }
            else
            {
                MessageBox.Show("Page name exist. Please choose an others");
            }
        }

        private ICommand _removePageCommand;

        public ICommand RemovePageCommand => _removePageCommand ?? (_removePageCommand =
                                        new DelegateCommand<PageModel>(ExecuteRemovePageCommand, page => page != null && !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteRemovePageCommand(PageModel page)
        {
            Pages.Remove(page);
        }

        private PageModel GetPageByName(string name)
        {
            return Pages.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
