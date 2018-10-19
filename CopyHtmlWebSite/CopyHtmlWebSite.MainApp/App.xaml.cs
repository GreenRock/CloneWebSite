using System.Windows;
using CopyHtmlWebSite.Core.Infrastructure;
using CopyHtmlWebSite.MainApp.Services.Converts;
using CopyHtmlWebSite.MainApp.Services.DataStorages;
using CopyHtmlWebSite.MainApp.Services.DialogServices;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;
using CopyHtmlWebSite.MainApp.Services.SiteFactories;
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

            containerRegistry.RegisterForNavigation<MainWindow>(nameof(MainWindow));
            containerRegistry.RegisterForNavigation<CreateNewSiteUserControl>(nameof(CreateNewSiteUserControl));
            containerRegistry.RegisterForNavigation<MainUserControl>(nameof(MainUserControl));
            containerRegistry.RegisterForNavigation<RightMenuUserControl>(nameof(RightMenuUserControl));
            containerRegistry.RegisterForNavigation<SettingsUserControl>(nameof(SettingsUserControl));
            containerRegistry.RegisterForNavigation<AboutUserControl>(nameof(AboutUserControl));
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            var settingsService = Container.Resolve<ISettingsService>();
            settingsService?.InitSettings();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(Regions.MainRegion, nameof(MainUserControl));
            regionManager.RequestNavigate(Regions.RightRegion, nameof(RightMenuUserControl));

            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Show();
        }
    }
}
