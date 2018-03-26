namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Prism.Regions;
    using Properties;
    using ViewModelBases;
    public class AboutUserControlViewModel : NavigationViewModelBase
    {
        public AboutUserControlViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Owner = Settings.Default.Owner;
            Email = Settings.Default.Email;
        }

        private string _owner;
        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }	
    }
}
