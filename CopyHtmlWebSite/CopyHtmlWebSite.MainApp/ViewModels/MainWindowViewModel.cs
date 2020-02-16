using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;
namespace CopyHtmlWebSite.MainApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Title => string.Join(" - ", "Copy Html Website", Properties.Settings.Default.ApplicationName, Properties.Settings.Default.Version);

    }
}
