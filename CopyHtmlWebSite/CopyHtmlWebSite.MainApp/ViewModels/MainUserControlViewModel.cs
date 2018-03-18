namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Forms;
    using System.Windows.Input;
    using Models;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Regions;
    using ViewModelBases;

    public class MainUserControlViewModel : NavigationViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        public MainUserControlViewModel(IRegionManager regionManager, 
                                        IEventAggregator eventAggregator) 
            : base(regionManager)
        {
            _eventAggregator = eventAggregator;
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
    }
}
