using System.Windows;
using CopyHtmlWebSite.Core.Infrastructure;
using CopyHtmlWebSite.MainApp.Helpers;
using CopyHtmlWebSite.MainApp.Services.Converts;
using CopyHtmlWebSite.MainApp.Services.DataStorages;
using CopyHtmlWebSite.MainApp.Services.DialogServices;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;
using CopyHtmlWebSite.MainApp.Services.SiteFactories;
using CopyHtmlWebSite.MainApp.ViewModels.Settings;
using CopyHtmlWebSite.MainApp.Views;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;

namespace CopyHtmlWebSite.MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDataStorage, MemoryDataStorage>();
            containerRegistry.RegisterSingleton<IConverter, AutoMapperConvert>();
            containerRegistry.RegisterSingleton<IDialogService, DialogService>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();
            containerRegistry.RegisterSingleton<ISiteService, SiteService>();

            containerRegistry.RegisterForNavigation<MainWindow>(PageConstants.MainWindow);
            containerRegistry.RegisterForNavigation<CreateNewSiteUserControl>(PageConstants.CreateNewSite);
            containerRegistry.RegisterForNavigation<MainUserControl>(PageConstants.MainUserControl);
            containerRegistry.RegisterForNavigation<RightMenuUserControl>(PageConstants.RightMenu);
            containerRegistry.RegisterForNavigation<SettingsUserControl, SettingsUserControlViewModel>(PageConstants.Settings);
            containerRegistry.RegisterForNavigation<AboutUserControl>(PageConstants.About);
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(Regions.MainRegion, nameof(MainUserControl));
            regionManager.RequestNavigate(Regions.RightRegion, nameof(RightMenuUserControl));

            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Show();
        }
    }
}
