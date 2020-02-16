using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.ViewModels;

namespace CopyHtmlWebSite.MainApp.Extensions
{
    public static class SelectedItemExtension
    {
        public static SelectedItemViewModel ToSelectedItemViewModel(this ISelectedItem item)
        {
            return new SelectedItemViewModel
            {
                Id = item.Id,
                Value = item.Value,
                Text = item.Text,
                Selected = item.Selected
            };
        }
    }
}