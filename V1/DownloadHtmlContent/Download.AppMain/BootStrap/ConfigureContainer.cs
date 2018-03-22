namespace Download.AppMain.BootStrap
{
    using Unity;
    using CommonServiceLocator;
    public static class ConfigureContainer
    {
        public static IUnityContainer BuildUnityContainer()
        {
            IUnityContainer container = new UnityContainer();

            ExtensionBootStrap.ExtensionInit(container);

            FormBootStrap.FormBootStrapInit(container);

            return container;
        }

    }
}
