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
                                        new DelegateCommand(ExecuteHomeCommand, () => !IsStart && !IsBusy)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => IsStart));

        private void ExecuteHomeCommand()
        {
            NavigateTo(nameof(MainUserControl));
        }

        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ?? (_startCommand =
                                        new DelegateCommand(ExecuteStartCommand, () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteStartCommand()
        {
           
        }

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
                                        new DelegateCommand(ExecuteAddNewSiteCommand, () => !IsStart && !IsBusy)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => IsStart));

        private void ExecuteAddNewSiteCommand()
        {
            NavigateTo(nameof(CreateNewSiteUserControl));
        }

        private ICommand _settingsCommand;

        public ICommand SettingsCommand => _settingsCommand ?? (_settingsCommand =
                                        new DelegateCommand(ExecuteSettingsCommand, () => !IsStart && !IsBusy)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => IsStart));
        private void ExecuteSettingsCommand()
        {
            NavigateTo(nameof(SettingsUserControl));
        }
    }
}
