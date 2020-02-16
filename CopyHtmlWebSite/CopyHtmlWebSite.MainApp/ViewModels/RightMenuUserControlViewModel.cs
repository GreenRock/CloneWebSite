using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CopyHtmlWebSite.MainApp.Extensions;
using CopyHtmlWebSite.MainApp.Helpers;
using CopyHtmlWebSite.MainApp.Models;
using CopyHtmlWebSite.MainApp.Properties;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Prism.Commands;
    using System.Windows.Input;
    using Core.Models;
    using Prism.Regions;
    using Services.SiteFactories;
    using ViewModelBases;
    using Views;

    public class RightMenuUserControlViewModel : NavigationViewModelBase
    {

        public RightMenuUserControlViewModel(
            IRegionManager regionManager)
            : base(regionManager)
        {
            LoadItems();
        }

        public ObservableCollection<MenuItemModel> Items { get; } = new ObservableCollection<MenuItemModel>();

        private void LoadItems()
        {
            Items.Add(new MenuItemModel
            {
                Image = Resources.home.GetBitmapSource(),
                Display = Resources.HomeToolTip,
                PageName = PageConstants.MainUserControl,
                Order = 0
            });
            Items.Add(new MenuItemModel
            {
                Image = Resources.create_new.GetBitmapSource(),
                Display = Resources.CreateANewSiteToolTip,
                PageName = PageConstants.CreateNewSite,
                Order = 5
            });
            Items.Add(new MenuItemModel
            {
                Image = Resources.settings.GetBitmapSource(),
                Display = Resources.SettingsToolTip,
                PageName = PageConstants.Settings,
                Order = 10
            });
            Items.Add(new MenuItemModel
            {
                Image = Resources.contact.GetBitmapSource(),
                Display = Resources.AboutToolTip,
                PageName = PageConstants.About,
                Order = 15
            });
        }

        private ICommand _itemSelectedCommand;
        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand =
                                        new DelegateCommand<MenuItemModel>(async item => await ExecuteItemSelectedCommandAsync(item), item => !IsBusy)
                                            .ObservesProperty(() => IsBusy));

        Task ExecuteItemSelectedCommandAsync(MenuItemModel item)
        {
            return SafeExecuteInvoke(() =>
            {
                NavigateTo(item.PageName);
                return Task.CompletedTask;
            });
        }
    }
}
