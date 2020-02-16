using System.Threading.Tasks;
using System.Windows.Input;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings.Base
{
    public interface ISettingItemViewModel
    {
        Task Init();
        ICommand SaveCommand { get; }
    }
}
