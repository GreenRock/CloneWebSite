namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using System.ComponentModel;
    using Prism.Regions;
    using Properties;
    using ViewModelBases;
    using Views;

    public class MainWindowViewModel : ViewModelBase
    {
        public string Title => string.Join(" - ", "Copy Html Website", Settings.Default.ApplicationName, Settings.Default.Version);
        
    }
}
