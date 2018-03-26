namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System.Windows;
    using Prism.Commands;
    using System.Windows.Input;
    using Prism.Regions;
    using ViewModelBases;
    using Views;

    public class RightMenuUserControlViewModel : NavigationViewModelBase
    {
        public RightMenuUserControlViewModel(IRegionManager regionManager) 
            : base(regionManager)
        {

        }

        private ICommand _homeCommand;

        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(MainUserControl)), () => !IsStart && !IsBusy)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => IsStart));

        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ?? (_startCommand =
                                        new DelegateCommand(() => SetIsStart(!IsStart), () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private bool _isStart;
        public bool IsStart
        {
            get => _isStart;
            set => SetProperty(ref _isStart, value);
        }

        private void SetIsStart(bool val)
        {
            Application.Current.Dispatcher.Invoke(() => { IsStart = val; });
        }


        private ICommand _addNewSiteCommand;

        public ICommand AddNewSiteCommand => _addNewSiteCommand ?? (_addNewSiteCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(CreateNewSiteUserControl)), () => !IsStart && !IsBusy)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => IsStart));

        private ICommand _settingsCommand;

        public ICommand SettingsCommand => _settingsCommand ?? (_settingsCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(SettingsUserControl)), () => !IsStart && !IsBusy)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => IsStart));

        private ICommand _aboutCommand;

        public ICommand AboutCommand => _aboutCommand ?? (_aboutCommand =
                                        new DelegateCommand(() => NavigateTo(nameof(AboutUserControl)), () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));
    }
}
