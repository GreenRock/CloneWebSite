namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Prism.Commands;
    using System.Windows.Input;
    using Core.Models;
    using Prism.Regions;
    using Services.SiteFactories;
    using ViewModelBases;
    using Views;

    public class RightMenuUserControlViewModel : NavigationViewModelBase
    {

        public RightMenuUserControlViewModel(
            IRegionManager regionManager)
            : base(regionManager)
        {

        }

        private ICommand _homeCommand;

        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(MainUserControl)), () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private ICommand _addNewSiteCommand;

        public ICommand AddNewSiteCommand => _addNewSiteCommand ?? (_addNewSiteCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(CreateNewSiteUserControl)), () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private ICommand _settingsCommand;

        public ICommand SettingsCommand => _settingsCommand ?? (_settingsCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(SettingsUserControl)), () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private ICommand _aboutCommand;

        public ICommand AboutCommand => _aboutCommand ?? (_aboutCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(AboutUserControl)), () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));
    }
}
