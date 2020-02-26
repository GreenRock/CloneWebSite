using System;
using System.Linq;
using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.Core.Infrastructure;
using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.Models;
using CopyHtmlWebSite.MainApp.Properties;
using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;
using CopyHtmlWebSite.MainApp.Views;
using Prism.Commands;
using Prism.Regions;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using CopyHtmlWebSite.Core.Models.Service;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
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

            Page = new PageViewModel();
            Site = new SiteViewModel();
            PageCollection = new PageCollection
            {
                OnChanged = args =>
                {
                    AddSiteCommand.RaiseCanExecuteChanged();
                }
            };
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Init();
        }

        private void Init()
        {
            PageCollection.Clear();
            Page.Reset();
        }

        private bool _isOpenAddSource;
        public bool IsOpenAddSource
        {
            get => _isOpenAddSource;
            set => SetProperty(ref _isOpenAddSource, value);
        }

        public SiteViewModel Site { get; }

        public PageCollection PageCollection { get; }

        public PageViewModel Page { get; set; }

        private DelegateCommand<SiteViewModel> _addSiteCommand;
        public DelegateCommand<SiteViewModel> AddSiteCommand => _addSiteCommand ?? (_addSiteCommand =
                                              new DelegateCommand<SiteViewModel>(ExecuteAddSiteCommand, item => CanExecuteCommand()).ObservesProperty(() => IsBusy));

        private async void ExecuteAddSiteCommand(SiteViewModel site)
        {
            if (!CanAddSite(site)) return;

            await SafeExecuteInvoke(async () =>
            {
                var siteModel = new SiteModel
                {
                    SiteName = site.SiteName,
                   // Pages = PageCollection,
                    RootSite = site.RootSite
                };

                var result = await _dataStorage.AddSite(siteModel);
                if (result.IsSuccess())
                {
                    NavigateTo(nameof(MainUserControl));
                    return;
                }
                MessageBox.Show(result.ErrorMessage);
            });
        }

        private bool CanAddSite(SiteViewModel site)
        {
            if (site != null && site.SiteName.HasText()) return true;
            return PageCollection.Any();
        }

        private ICommand _addNewPageCommand;
        public ICommand AddNewPageCommand => _addNewPageCommand ?? (_addNewPageCommand =
                                        new DelegateCommand<PageViewModel>(ExecuteAddNewPageCommand, page => CanExecuteCommand())
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteAddNewPageCommand(PageViewModel page)
        {
            var pageSource = page.PageSource;

            if (pageSource.HasText() && Regex.IsMatch(pageSource, RegexResource.Url))
            {
                page.IsHtml = false;
                AddAPage(page);
            }
        }

        private ICommand _addPageWithSourceCommand;
        public ICommand AddPageWithSourceCommand => _addPageWithSourceCommand ?? (_addPageWithSourceCommand =
                                        new DelegateCommand<PageViewModel>(ExecuteAddPageWithSourceCommand, page => CanExecuteCommand())
                                                .ObservesProperty(() => IsBusy));

        private void ExecuteAddPageWithSourceCommand(PageViewModel page)
        {
            page.IsHtml = true;
            AddAPage(page);
        }

        private ICommand _showPageSourceCommand;
        public ICommand ShowPageSourceCommand => _showPageSourceCommand ?? (_showPageSourceCommand =
                                        new DelegateCommand(ExecuteShowPageSourceCommand, CanExecuteCommand).ObservesProperty(() => IsBusy));

        private void ExecuteShowPageSourceCommand()
        {
            IsOpenAddSource = !IsOpenAddSource;
        }

        private ICommand _removePageCommand;

        public ICommand RemovePageCommand => _removePageCommand ?? (_removePageCommand =
                                        new DelegateCommand<PageViewModel>(ExecuteRemovePageCommand, item => CanExecuteCommand())
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteRemovePageCommand(PageViewModel page)
        {
            if (page != null)
            {
                PageCollection.Remove(page);
            }
        }

        private void AddAPage(PageViewModel pageModel)
        {
            var page = _converter.Map<PageViewModel, PageViewModel>(pageModel);

            page.Id = Guid.NewGuid().ToString();

            var pageName = VerifyPageName(page.PageName);
            if (pageName.HasNoText())
            {
                pageName = VerifyPageName(page.PageSource);
            }

            page.PageName = pageName;
            PageCollection.Add(page);
            Page.Reset();
        }


        private string VerifyPageName(string pageName)
        {
            pageName = Regex.Replace(pageName, RegexResource.SpecialChar, string.Empty);
            var page = PageCollection.GetByName(pageName);
            if (page == null) return pageName;

            var tempPage = string.Empty;
            var index = 1;
            while (tempPage.HasNoText())
            {
                tempPage = $"{page.PageName}_{index}";
                if (PageCollection.GetByName(tempPage) != null)
                {
                    index++;
                    tempPage = string.Empty;
                }
            }
            return tempPage;
        }

        private bool CanExecuteCommand()
        {
            return !IsBusy;
        }
    }
}
