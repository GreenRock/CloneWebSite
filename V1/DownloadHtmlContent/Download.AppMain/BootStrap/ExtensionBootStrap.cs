namespace Download.AppMain.BootStrap
{
    using Common.Extensions;
    using Download.Services.CommandServices;
    using Download.Services.WebClientServices;
    using Services;
    using Unity;

    public class ExtensionBootStrap
    {
        public static void ExtensionInit(IUnityContainer container)
        {
            container.RegisterType<FileExtension>();
            container.RegisterType<UrlExtenstion>();
            container.RegisterType<ImageExtension>();
            container.RegisterType<RegexExtension>();
            container.RegisterType<StringExtension>();
            container.RegisterType<IWebClientService, WebClientService>();
            container.RegisterType<ICommandService, CommandService>();
            container.RegisterType<DirectoryExtension>();
            container.RegisterType<HtmlAgilityPackExtension>();

            container.RegisterType<AnalyzeService>();
            container.RegisterType<HandleTagService>();
            container.RegisterType<HandleFileService>();
            container.RegisterType<SettingService>();
        }
    }
}
