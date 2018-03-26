namespace CopyHtmlWebSite.MainApp.Services.SettingsServices
{
    public interface ISettingsService
    {
        void InitSettings();
        string GetSettingsByKey(string key);
        void UpdateSettingsByKey(string key, string value);
    }
}