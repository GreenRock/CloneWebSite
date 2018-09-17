using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CopyHtmlWebSite.MainApp.Services.SiteFactories;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Core.Extensions;
    using Core.Infrastructure;
    using Core.Models;
    using Prism.Commands;
    using Prism.Regions;
    using Services.DialogServices;
    using Services.SettingsServices;
    using ViewModelBases;

    public class MainUserControlViewModel : NavigationViewModelBase
    {
        private readonly IRunGetSite _runGetSite;
        private readonly ISettingsService _settingsService;
        private readonly IDialogService _dialogService;
        private readonly IDataStorage _dataStorage;
        public MainUserControlViewModel(IRegionManager regionManager,
            IDataStorage dataStorage,
            IDialogService dialogService,
            ISettingsService settingsService,
            IRunGetSite runGetSite)
            : base(regionManager)
        {
            _dataStorage = dataStorage;
            _dialogService = dialogService;
            _settingsService = settingsService;
            _dataStorage = dataStorage;
            _runGetSite = runGetSite;
            _runGetSite.OnStart += OnStart;
            _runGetSite.OnError += OnError;
            _runGetSite.OnFinish += OnFinish;
            _runGetSite.OnStatusChanged += OnStatusChanged;
            Sites = new ObservableCollection<SiteModel>();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Init();
        }

        private void Init()
        {
            try
            {
                SetBusy();
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
            finally
            {
                ClearBusy();
            }
        }

        private ICommand _chooseFolderCommand;

        public ICommand ChooseFolderCommand => _chooseFolderCommand ?? (_chooseFolderCommand =
                                                   new DelegateCommand(ExecuteChooseFolderCommand, () => !IsBusy)
                                                       .ObservesProperty(() => IsBusy));
        private void ExecuteChooseFolderCommand()
        {
            try
            {
                SetBusy();
                var result = _dialogService.ChooseFolder();
                if (result.HasText())
                {
                    SaveToFolder = result;
                }
            }
            finally
            {
                ClearBusy();
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
            try
            {
                SetBusy();
                Sites.Remove(site);
                _dataStorage.RemoveSite(site);
            }
            finally
            {
                ClearBusy();
            }
        }

        private void LoadSettings()
        {
            SaveToFolder = _settingsService.GetSettingsByKey(SettingsConst.SaveTo);
        }

        private bool _isStart;
        public bool IsStart
        {
            get => _isStart;
            set => SetProperty(ref _isStart, value);
        }

        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ?? (_startCommand =
                                            new DelegateCommand(async () => await ExecuteStartCommand(), () => !IsBusy && Sites.Any())
                                                .ObservesProperty(() => IsBusy)
                                                .ObservesProperty(() => Sites));

        private async Task ExecuteStartCommand()
        {
            try
            {
                SetStart();
                SetBusy();

                var sites = _dataStorage.GetSites();
                foreach (var site in sites)
                {
                    IRunGetSite runGetSite = _runGetSite;
                    await runGetSite.Run(site);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                ClearStart();
                ClearBusy();
            }
        }

        private void SetStart()
        {
            SetWithTask(() => { IsStart = true; });
        }

        private void ClearStart()
        {
            SetWithTask(() => { IsStart = false; });
        }

        private void OnFinish(SiteModel site, FinishedSiteResult result)
        {
            throw new NotImplementedException();
        }

        private void OnError(SiteModel site, string errorMessage)
        {
            throw new NotImplementedException();
        }

        private void OnStart(SiteModel site)
        {
            throw new NotImplementedException();
        }

        private void OnStatusChanged(SiteModel site, SiteStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
