namespace CopyHtmlWebSite.MainApp.Services.SettingsServices
{
    using System;
    using Core.Extensions;
    using Properties;

    public class SettingsService : ISettingsService
    {
        public void InitSettings()
        {
            SetDefaultIfNot(SettingsConst.SaveTo, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            SetDefaultIfNot(SettingsConst.Folder.CssFolder, "css");
            SetDefaultIfNot(SettingsConst.Folder.JsFolder, "js");
            SetDefaultIfNot(SettingsConst.Folder.ImageFolder, "image");
            SetDefaultIfNot(SettingsConst.Folder.FontFolder, "font");
        }

        public string GetSettingsByKey(string key)
        {
            var o = Settings.Default[key];
            if (o == null)
                return string.Empty;
            return (string)o;
        }


        public void UpdateSettingsByKey(string key, string value)
        {
            if (value.HasText())
            {
                Settings.Default[key] = value;
                Settings.Default.Save();
            }
        }

        private void SetDefaultIfNot(string key, string defaultvaule)
        {
            if (GetSettingsByKey(key).HasNotText())
            {
                UpdateSettingsByKey(key, defaultvaule);
            }
        }
    }
}