using Prism.Mvvm;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Prism.Regions;
    using ViewModelBases;

    public class CreateNewSiteUserControlViewModel : NavigationViewModelBase
    {
        public CreateNewSiteUserControlViewModel(IRegionManager regionManager) 
            : base(regionManager)
        {

        }
    }
}
