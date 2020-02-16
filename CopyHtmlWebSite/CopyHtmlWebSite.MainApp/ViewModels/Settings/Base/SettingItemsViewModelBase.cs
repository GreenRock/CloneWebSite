using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.Extensions;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;
using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;
using Prism.Commands;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings.Base
{
    public abstract class SettingItemsViewModelBase : ViewModelBase, ISettingItemsViewModel
    {
        protected readonly ISettingsService SettingsService;
        protected SettingItemsViewModelBase(ISettingsService settingsService)
        {
            SettingsService = settingsService;
        }

        public Task Init()
        {
            return SafeExecuteInvoke(async () =>
            {
                Items.Clear();
                var result = await LoadItems();
                if (result.IsNotNullOrEmpty())
                {
                    foreach (var item in result)
                    {
                        Items.Add(ToItems(item));
                    }
                }
            });
        }

        public virtual string Title { get; set; }

        public ObservableCollection<ISelectedItem> Items { get; } = new ObservableCollection<ISelectedItem>();

        protected virtual ISelectedItem ToItems(ISelectedItem item)
        {
            return item.ToSelectedItemViewModel();
        }

        protected abstract Task<IEnumerable<ISelectedItem>> LoadItems();

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand =
                                           new DelegateCommand(async () => await ExecuteSaveCommandAsync(), CanExecuteSaveCommand)
                                               .ObservesProperty(() => IsBusy));

        private bool CanExecuteSaveCommand()
        {
            return Items.IsNotNullOrEmpty() && !IsBusy;
        }

        Task ExecuteSaveCommandAsync()
        {
            return SafeExecuteInvoke(() =>
            {
                SettingsService.UpdateSetting(Items);
                return Task.CompletedTask;
            });
        }
    }
}