namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Forms;
    using System.Windows.Input;
    using Models;
    using Prism.Commands;
    using Prism.Regions;
    using Services.DataStorages;
    using ViewModelBases;

    public class MainUserControlViewModel : NavigationViewModelBase
    {
        private readonly IDataStorage _dataStorage;
        public MainUserControlViewModel(IRegionManager regionManager, 
            IDataStorage dataStorage) 
            : base(regionManager)
        {
            _dataStorage = dataStorage;
            Sites = new ObservableCollection<SiteModel>();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Sites.Clear();

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
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = @"Please choose a folder";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SaveToFolder = dialog.SelectedPath;
                }
            }
        }

        private string _saveToFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
                                        new DelegateCommand<SiteModel>(ExecuteRemoveSiteCommand, (site) => site != null && !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteRemoveSiteCommand(SiteModel site)
        {
            Sites.Remove(site);
        }
    }
}
