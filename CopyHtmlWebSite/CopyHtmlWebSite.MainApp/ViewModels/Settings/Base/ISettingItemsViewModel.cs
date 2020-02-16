using System.Collections.ObjectModel;
using CopyHtmlWebSite.Core.Models;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings.Base
{
    public interface ISettingItemsViewModel : ISettingItemViewModel
    {
        string Title { get; set; }
        ObservableCollection<ISelectedItem> Items { get; }
    }
}