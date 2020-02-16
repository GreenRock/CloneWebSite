using System.Collections.Generic;
using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.Properties;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;
using CopyHtmlWebSite.MainApp.ViewModels.Settings.Base;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings
{
    public class SettingIgnoreViewModel : SettingItemsViewModelBase
    {
        public SettingIgnoreViewModel(ISettingsService settingsService) : base(settingsService)
        {
        }

        public override string Title { get; set; } = Resources.IgnoreGroupLabel;

        protected override async Task<IEnumerable<ISelectedItem>> LoadItems()
        {
           return await SettingsService.GetSettingIgnore();
        }
    }
}
