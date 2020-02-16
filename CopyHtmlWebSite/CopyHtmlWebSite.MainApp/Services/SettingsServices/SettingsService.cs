using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.Properties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.MainApp.Services.SettingsServices
{
    public class SettingsService : ISettingsService
    {
        public Task<string> GetSettingsByKey(string key)
        {
            var o = Settings.Default[key];
            if (o == null)
                return Task.FromResult(string.Empty);
            return Task.FromResult((string)o);
        }


        public Task UpdateSettingsByKey(string key, string value)
        {
            if (value.HasText())
            {
                Settings.Default[key] = value;
                Settings.Default.Save();
            }

            return Task.CompletedTask;
        }

        public async Task UpdateSetting(IEnumerable<ISelectedItem> items)
        {
            var task = new List<Task>();
            foreach (var item in items)
            {
                task.Add(UpdateSettingsByKey(item.Id, item.Value));
            }

            await Task.WhenAll(task);
        }

        public async Task<List<ISelectedItem>> GetSettingFolder()
        {
            var selectedItems = new List<ISelectedItem>
            {
               await GetItem(nameof(Settings.Default.CssFolder), Resources.CssFolderLabel),
               await GetItem(nameof(Settings.Default.JsFolder), Resources.JsFolderLabel),
               await GetItem(nameof(Settings.Default.ImagesFolder), Resources.ImagesFolderLabel),
               await GetItem(nameof(Settings.Default.FontsFolder), Resources.FontsFolderLabel)
            };
            return selectedItems;
        }

        public async Task<List<ISelectedItem>> GetSettingIgnore()
        {
            var selectedItems = new List<ISelectedItem>
            {
               await GetItem(nameof(Settings.Default.IgnoreAttributes), Resources.AttributesIgnoreLabel),
               await GetItem(nameof(Settings.Default.IgnoreFile), Resources.FileIgnoreLabel),
               await GetItem(nameof(Settings.Default.IgnoreUrl), Resources.UrlIgnoreLabel),
               await GetItem(nameof(Settings.Default.IgnoreContent), Resources.ContentIgnoreLabel)
            };
            return selectedItems;
        }

        public Task<string> GetSettingSaveTo()
        {
            return GetSettingsByKey(nameof(Settings.Default.SaveTo));
        }

        private async Task<SelectedItem> GetItem(string key, string label)
        {
            return new SelectedItem
            {
                Id = key,
                Value = await GetSettingsByKey(key),
                Text = label
            };
        }
    }
}