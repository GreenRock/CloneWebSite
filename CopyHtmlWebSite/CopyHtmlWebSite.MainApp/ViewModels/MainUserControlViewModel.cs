namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Core.Extensions;
    using Core.Infrastructure;
    using Core.Models;
    using Prism.Commands;
    using Prism.Regions;
    using Properties;
    using Services.DialogServices;
    using Services.SettingsServices;
    using ViewModelBases;

    public class MainUserControlViewModel : NavigationViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IDialogService _dialogService;
        private readonly IDataStorage _dataStorage;
        public MainUserControlViewModel(IRegionManager regionManager, 
            IDataStorage dataStorage, 
            IDialogService dialogService, 
            ISettingsService settingsService) 
            : base(regionManager)
        {
            _dataStorage = dataStorage;
            _dialogService = dialogService;
            _settingsService = settingsService;
            Sites = new ObservableCollection<SiteModel>();
            LoadSettings(); // For first time application starting
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Sites.Clear();
            LoadSettings();
             var sites = _dataStorage.GetSites();
            if (sites != null)
            {
                foreach (var site in sites)
                {
                    Sites.Add(site);
                }
            }
        }

        private ICommand _chooseFolderCommand;

        public ICommand ChooseFolderCommand => _chooseFolderCommand ?? (_chooseFolderCommand =
                                                   new DelegateCommand(ExecuteChooseFolderCommand, () => !IsBusy)
                                                       .ObservesProperty(() => IsBusy));
        private void ExecuteChooseFolderCommand()
        {
            var result = _dialogService.ChooseFolder();
            if (result.HasText())
            {
                SaveToFolder = result;
            }
        }

        private string _saveToFolder;
        public string SaveToFolder
        {
            get => _saveToFolder;
            set => SetProperty(ref _saveToFolder, value);
        }

        private ObservableCollection<SiteModel> _sites;
        public ObservableCollection<SiteModel> Sites
        {
            get => _sites;
            set => SetProperty(ref _sites, value);
        }

        private ICommand _removeSiteCommand;

        public ICommand RemoveSiteCommand => _removeSiteCommand ?? (_removeSiteCommand =
                                        new DelegateCommand<SiteModel>(ExecuteRemoveSiteCommand, (site) => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteRemoveSiteCommand(SiteModel site)
        {
            Sites.Remove(site);
            _dataStorage.RemoveSite(site);
        }

        private void LoadSettings()
        {
            SaveToFolder = _settingsService.GetSettingsByKey(SettingsConst.SaveTo);
        }
    }
}
