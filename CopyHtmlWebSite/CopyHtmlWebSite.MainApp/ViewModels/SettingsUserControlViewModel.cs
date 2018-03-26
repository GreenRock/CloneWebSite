using Prism.Commands;
namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System.Windows.Data;
    using System.Windows.Input;
    using Core.Extensions;
    using Prism.Regions;
    using Properties;
    using Services.DialogServices;
    using Services.SettingsServices;
    using ViewModelBases;

    public class SettingsUserControlViewModel : NavigationViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IDialogService _dialogService;
        public SettingsUserControlViewModel(IRegionManager regionManager, 
            IDialogService dialogService, 
            ISettingsService settingsService) 
            : base(regionManager)
        {
            _dialogService = dialogService;
            _settingsService = settingsService;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            var folder = new FolderSettings()
            {
                Css = _settingsService.GetSettingsByKey(SettingsConst.Folder.CssFolder),
                Js = _settingsService.GetSettingsByKey(SettingsConst.Folder.JsFolder),
                Image = _settingsService.GetSettingsByKey(SettingsConst.Folder.ImageFolder),
                Font = _settingsService.GetSettingsByKey(SettingsConst.Folder.FontFolder)
            };

            Folder = folder;
            SaveToFolder = _settingsService.GetSettingsByKey(SettingsConst.SaveTo);
        }

        private string _saveToFolder;
        public string SaveToFolder
        {
            get => _saveToFolder;
            set => SetProperty(ref _saveToFolder, value);
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
                _settingsService.UpdateSettingsByKey(SettingsConst.SaveTo, result);
                SaveToFolder = result;
            }
        }

        private ICommand _saveFolderSettingsCommand;
        public ICommand SaveFolderSettingsCommand => _saveFolderSettingsCommand ?? (_saveFolderSettingsCommand =
                                        new DelegateCommand(ExecuteSaveFolderSettingsCommand, () => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        private void ExecuteSaveFolderSettingsCommand()
        {
            _settingsService.UpdateSettingsByKey(SettingsConst.Folder.CssFolder, Folder.Css);
            _settingsService.UpdateSettingsByKey(SettingsConst.Folder.JsFolder, Folder.Js);
            _settingsService.UpdateSettingsByKey(SettingsConst.Folder.ImageFolder, Folder.Image);
            _settingsService.UpdateSettingsByKey(SettingsConst.Folder.FontFolder, Folder.Font);
        }

        private FolderSettings _folder;
        public FolderSettings Folder
        {
            get => _folder;
            set => SetProperty(ref _folder, value);
        }

        public class FolderSettings
        {
            public string Css { get; set; }
            public string Js { get; set; }
            public string Image { get; set; }
            public string Font { get; set; }
        }
    }
}
