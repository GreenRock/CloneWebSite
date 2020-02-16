using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.MainApp.Services.DialogServices;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;
using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;
using Prism.Regions;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings
{
    public class SettingsUserControlViewModel : NavigationViewModelBase
    {
        public SettingsUserControlViewModel(IRegionManager regionManager, 
            IDialogService dialogService, 
            ISettingsService settingsService) 
            : base(regionManager)
        {
            SaveTo = new SettingSaveToViewModel(settingsService, dialogService);
            Ignore = new SettingIgnoreViewModel(settingsService);
            Folder = new SettingFolderViewModel(settingsService);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            Init();
        }

        private void Init()
        {
            SafeExecuteInvoke(() => Task.WhenAll(SaveTo.Init(), Ignore.Init(), Folder.Init())).SafeFireAndForget();
        }

        private SettingSaveToViewModel _saveTo;
        public SettingSaveToViewModel SaveTo
        {
            get => _saveTo;
            set => SetProperty(ref _saveTo, value);
        }

        private SettingIgnoreViewModel _ignore;
        public SettingIgnoreViewModel Ignore
        {
            get => _ignore;
            set => SetProperty(ref _ignore, value);
        }

        private SettingFolderViewModel _folder;
        public SettingFolderViewModel Folder
        {
            get => _folder;
            set => SetProperty(ref _folder, value);
        }
    }
}
