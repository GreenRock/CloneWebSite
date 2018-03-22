namespace Download.AppMain
{
    using System;
    using System.Windows.Forms;
    using BootStrap;
    using Models;
    using Services;
    using CommonServiceLocator;
    static class Program
    {
        public static string SettingFile = "setting.json";
        public static SettingModel Settings { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = ConfigureContainer.BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SettingService settingService = (SettingService)container.Resolve(typeof(SettingService), null);
            Settings = settingService.Init(SettingFile);

            ;

            Application.Run((FrmMain)container.Resolve(typeof(FrmMain), null));
        }
    }
}
