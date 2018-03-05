namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Forms;
    using System.Windows.Input;
    using Models;
    using Prism.Commands;

    public class MainWindowViewModel : ViewModelBase
    {
        protected override string ViewName => "Copy Html Website";

        private bool _isStart;
        public bool IsStart
        {
            get => _isStart;
            set => SetProperty(ref _isStart, value);
        }

        private ICommand _startCommand;
        public ICommand StartCommand => _startCommand ?? (_startCommand =
                                           new DelegateCommand(ExecuteStartCommand, () => !IsBusy && !IsStart)
                                            .ObservesProperty(() => IsBusy)
                                            .ObservesProperty(() => IsStart));

        private void ExecuteStartCommand()
        {
            IsStart = !IsStart;
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

        private ICommand _createSiteCommand;
        public ICommand CreateSiteCommand => _createSiteCommand ?? (_createSiteCommand =
                                        new DelegateCommand(ExecuteCreateSiteCommand, () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteCreateSiteCommand()
        {

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
