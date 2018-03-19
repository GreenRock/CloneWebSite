using CopyHtmlWebSite.MainApp.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace CopyHtmlWebSite.MainApp
{
    using System.Windows.Navigation;
    using Prism.Regions;
    using Services.DataStorages;
    using ViewModels;

    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
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
            Container.RegisterTypeForNavigation<MainWindow>(nameof(MainWindow));
            Container.RegisterTypeForNavigation<CreateNewSiteUserControl>(nameof(CreateNewSiteUserControl));
            Container.RegisterTypeForNavigation<MainUserControl>(nameof(MainUserControl));
            Container.RegisterTypeForNavigation<RightMenuUserControl>(nameof(RightMenuUserControl));
            Container.RegisterTypeForNavigation<SettingsUserControl>(nameof(SettingsUserControl));

        }
    }
}
