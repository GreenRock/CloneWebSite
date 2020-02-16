using System.Windows.Media;

namespace CopyHtmlWebSite.MainApp.Models
{
    public class MenuItemModel
    {
        public string Display { get; set; }
        public ImageSource Image { get; set; }
        public int Order { get; set; }
        public string PageName { get; set; }
    }
}