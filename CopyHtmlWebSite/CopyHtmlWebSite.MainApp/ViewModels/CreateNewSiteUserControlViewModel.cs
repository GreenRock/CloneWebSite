using Prism.Mvvm;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System.Windows.Input;
    using Core.Extensions;
    using Models;
    using Prism.Commands;
    using Prism.Regions;
    using ViewModelBases;

    public class CreateNewSiteUserControlViewModel : NavigationViewModelBase
    {
        public CreateNewSiteUserControlViewModel(IRegionManager regionManager) 
            : base(regionManager)
        {
        }

        private string _siteName;
        public string SiteName
        {
            get => _siteName;
            set => SetProperty(ref _siteName, value);
        }	

        private PageModel _page;
        public PageModel Page
        {
            get => _page;
            set => SetProperty(ref _page, value);
        }	

        private ICommand _addNewPageCommand;

        public ICommand AddNewPageCommand => _addNewPageCommand ?? (_addNewPageCommand =
                                        new DelegateCommand<PageModel>(ExecuteAddNewPageCommand, 
                                                page => !IsBusy)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => Page)
                                                 );

        private void ExecuteAddNewPageCommand(PageModel page)
        {

        }
    }
}
