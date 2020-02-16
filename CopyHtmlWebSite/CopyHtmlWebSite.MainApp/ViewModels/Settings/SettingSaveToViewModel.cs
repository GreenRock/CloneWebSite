using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.MainApp.Properties;
using CopyHtmlWebSite.MainApp.Services.DialogServices;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings
{
    public class SettingSaveToViewModel : SettingItemByKeyViewModel
    {
        private readonly IDialogService _dialogService;
        public SettingSaveToViewModel(ISettingsService settingsService, IDialogService dialogService) : base(settingsService, nameof(Properties.Settings.Default.SaveTo))
        {
            _dialogService = dialogService;
        }

        public override string Title { get; set; } = Resources.SaveToFolderLabel;

        protected override Task ExecuteSaveCommandAsync()
        {
            return SafeExecuteInvoke(() =>
            {
                var result = _dialogService.ChooseFolder();
                if (result.HasText())
                {
                    SettingsService.UpdateSettingsByKey(Key, result);
                    Value = result;
                }
                return Task.CompletedTask;
            });
        }
    }
}
