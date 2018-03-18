namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Prism.Regions;
    using Properties;
    using ViewModelBases;
    using Views;

    public class MainWindowViewModel : ViewModelBase
    {
        public string Title => "Copy Html Website" + " - " + Resources.ApplicationName;
        
        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(Regions.MainRegion, typeof(MainUserControl));
            regionManager.RegisterViewWithRegion(Regions.RightRegion, typeof(RightMenuUserControl));
        }
    }
}
