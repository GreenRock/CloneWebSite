namespace CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases
{
    using Prism.Regions;

    public class NavigationViewModelBase : ViewModelBase, INavigationAware
    {
        protected readonly IRegionManager _regionManager;
        public NavigationViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        protected virtual void NavigateTo(string url, string region = Regions.MainRegion)
        {
            _regionManager.RequestNavigate(region, url);
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}