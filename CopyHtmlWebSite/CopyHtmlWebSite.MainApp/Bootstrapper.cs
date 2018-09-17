namespace CopyHtmlWebSite.MainApp
{
    using System.Windows;
    using Core.Infrastructure;
    using Microsoft.Practices.Unity;
    using Prism.Regions;
    using Prism.Unity;
    using Services.Converts;
    using Services.DataStorages;
    using Services.DialogServices;
    using Services.SettingsServices;
    using Services.SiteFactories;
    using Views;

    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            var settingsService = Container.Resolve<ISettingsService>();
            settingsService?.InitSettings();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(Regions.MainRegion, nameof(MainUserControl));
            regionManager.RequestNavigate(Regions.RightRegion, nameof(RightMenuUserControl));

            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
           //var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        }



        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IDataStorage, MemoryDataStorage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IConverter, AutoMapperConvert>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRunGetSite, RunGetSite>(new TransientLifetimeManager());

            Container.RegisterTypeForNavigation<MainWindow>(nameof(MainWindow));
            Container.RegisterTypeForNavigation<CreateNewSiteUserControl>(nameof(CreateNewSiteUserControl));
            Container.RegisterTypeForNavigation<MainUserControl>(nameof(MainUserControl));
            Container.RegisterTypeForNavigation<RightMenuUserControl>(nameof(RightMenuUserControl));
            Container.RegisterTypeForNavigation<SettingsUserControl>(nameof(SettingsUserControl));
            Container.RegisterTypeForNavigation<AboutUserControl>(nameof(AboutUserControl));
        }
    }
}
