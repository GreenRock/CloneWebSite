namespace CopyHtmlWebSite.MainApp.ViewModels.Settings.Base
{
    public interface ISettingItemByKeyViewModel : ISettingItemViewModel
    { 
        string Title { get; set; }
        string Key { get; set; }
        string Value { get; set; }
    }
}