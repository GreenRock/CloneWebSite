namespace Download.AppMain.BootStrap
{
    using Unity;

    public class FormBootStrap
    {
        public static void FormBootStrapInit(IUnityContainer container)
        {
            container.RegisterType<FrmMain>();
            container.RegisterType<FrmCreatePage>();
            container.RegisterType<FrmAboutMe>();
            container.RegisterType<AddHtmlForm>();
        }
    }
}
