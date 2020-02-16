using System.Collections.Generic;
using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.Properties;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;
using CopyHtmlWebSite.MainApp.ViewModels.Settings.Base;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings
{
    public class SettingFolderViewModel : SettingItemsViewModelBase
    {
        public SettingFolderViewModel(ISettingsService settingsService) : base(settingsService)
        {
        }

        public override string Title { get; set; } = Resources.FolderGroupLabel;

        protected override async Task<IEnumerable<ISelectedItem>> LoadItems()
        {
           return await SettingsService.GetSettingFolder();
        }
    }
}
