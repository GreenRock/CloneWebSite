using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Models.Service;
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
        private readonly ISiteService _siteService;
        private readonly ISettingsService _settingsService;
        private readonly IDialogService _dialogService;
        private readonly IDataStorage _dataStorage;
        public MainUserControlViewModel(IRegionManager regionManager,
            IDataStorage dataStorage,
            IDialogService dialogService,
            ISettingsService settingsService,
            ISiteService siteService)
            : base(regionManager)
        {
            _dataStorage = dataStorage;
            _dialogService = dialogService;
            _settingsService = settingsService;
            _dataStorage = dataStorage;
            _siteService = siteService;
            Sites = new ObservableCollection<SiteModel>();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Init();
        }

        private async void Init()
        {
            await SafeExecuteInvoke(async () =>
              {
                  SetBusy();
                  Sites.Clear();
                  await LoadSettings();
                  var sites = await _dataStorage.GetSites();
                  if (sites.IsSuccess())
                  {
                      foreach (var site in sites.Data)
                      {
                          Sites.Add(site);
                      }
                  }
              });
        }

        private ICommand _chooseFolderCommand;

        public ICommand ChooseFolderCommand => _chooseFolderCommand ?? (_chooseFolderCommand =
                                                   new DelegateCommand(ExecuteChooseFolderCommand, CanExecuteCommand)
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
                                        new DelegateCommand<SiteModel>(ExecuteRemoveSiteCommand, item => CanExecuteCommand())
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

        private async Task LoadSettings()
        {
            SaveToFolder = await _settingsService.GetSettingSaveTo();
        }

        private bool _isStart;
        public bool IsStart
        {
            get => _isStart;
            set => SetProperty(ref _isStart, value);
        }

        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ?? (_startCommand =
                                            new DelegateCommand(async () => await ExecuteStartCommand(), CanExecuteStartCommand)
                                                .ObservesProperty(() => IsBusy));

        private bool CanExecuteStartCommand()
        {
            return !IsBusy && Sites.Any();
        }

        private async Task ExecuteStartCommand()
        {
            try
            {
                SetStart();
                SetBusy();

                var sites = await _dataStorage.GetSites();
                if (sites.IsSuccess())
                {
                    foreach (var site in sites.Data)
                    {
                        await _siteService.Run(site);
                    }
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

        private bool CanExecuteCommand()
        {
            return !IsBusy;
        }
    }
}
