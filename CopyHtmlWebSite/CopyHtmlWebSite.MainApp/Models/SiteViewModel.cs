using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;

namespace CopyHtmlWebSite.MainApp.Models
{
    public class SiteViewModel : ViewModelBase
    {
        private string _siteName;
        public string SiteName
        {
            get => _siteName;
            set => SetProperty(ref _siteName, value);
        }

        private string _rootSite;
        public string RootSite
        {
            get => _rootSite;
            set => SetProperty(ref _rootSite, value);
        }
    }
}