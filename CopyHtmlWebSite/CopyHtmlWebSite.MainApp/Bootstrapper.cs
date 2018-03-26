namespace CopyHtmlWebSite.MainApp
{
    using System;
    using System.Windows;
    using Core.Extensions;
    using Core.Infrastructure;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;
    using Properties;
    using Services;
    using Services.Converts;
    using Services.DataStorages;
    using Services.DialogServices;
    using Services.SettingsServices;
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
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        }



        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IDataStorage, MemoryDataStorage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IConverter, AutoMapperConvert>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<MainWindow>(nameof(MainWindow));
            Container.RegisterTypeForNavigation<CreateNewSiteUserControl>(nameof(CreateNewSiteUserControl));
            Container.RegisterTypeForNavigation<MainUserControl>(nameof(MainUserControl));
            Container.RegisterTypeForNavigation<RightMenuUserControl>(nameof(RightMenuUserControl));
            Container.RegisterTypeForNavigation<SettingsUserControl>(nameof(SettingsUserControl));

        }
    }
}
