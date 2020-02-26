using System;
using System.Threading.Tasks;
using System.Windows;

namespace CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases
{
    using Prism.Regions;

    public class NavigationViewModelBase : ViewModelBase, INavigationAware
    {
        protected readonly IRegionManager RegionManager;
        public NavigationViewModelBase(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        protected virtual void NavigateTo(string url, string region = Regions.MainRegion)
        {
            RegionManager.RequestNavigate(region, url);
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

        protected override async Task HandleError(Exception ex)
        {
            await base.HandleError(ex);
            MessageBox.Show(ex.Message);
        }
    }
}