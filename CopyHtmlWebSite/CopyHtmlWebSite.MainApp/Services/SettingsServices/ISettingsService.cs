using System.Collections.Generic;
using System.Threading.Tasks;
using CopyHtmlWebSite.Core.Models;

namespace CopyHtmlWebSite.MainApp.Services.SettingsServices
{
    public interface ISettingsService
    {
        Task<string> GetSettingsByKey(string key);
        Task UpdateSettingsByKey(string key, string value);
        Task UpdateSetting(IEnumerable<ISelectedItem> items);
        Task<List<ISelectedItem>> GetSettingFolder();
        Task<List<ISelectedItem>> GetSettingIgnore();
        Task<string> GetSettingSaveTo();
    }
}