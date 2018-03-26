namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Prism.Regions;
    using Properties;
    using ViewModelBases;
    using Views;

    public class MainWindowViewModel : ViewModelBase
    {
        public string Title => string.Join(" - ", "Copy Html Website", Settings.Default.ApplicationName, Settings.Default.Version);
        
        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(Regions.MainRegion, typeof(MainUserControl));
            regionManager.RegisterViewWithRegion(Regions.RightRegion, typeof(RightMenuUserControl));
        }
    }
}
