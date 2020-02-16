using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Input;
using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.MainApp.Services.SettingsServices;
using CopyHtmlWebSite.MainApp.ViewModels.Settings.Base;
using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;
using Prism.Commands;

namespace CopyHtmlWebSite.MainApp.ViewModels.Settings
{
    public class SettingItemByKeyViewModel : ViewModelBase, ISettingItemByKeyViewModel
    {
        protected readonly ISettingsService SettingsService;
        public SettingItemByKeyViewModel(ISettingsService settingsService, string key)
        {
            Key = key;
            SettingsService = settingsService;
        }

        public virtual Task Init()
        {
            return SafeExecuteInvoke(async () => { Value = await SettingsService.GetSettingsByKey(Key); });
        }

        public virtual string Title { get; set; }

        public string Key { get; set; }

        private string _value;
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand =
                                           new DelegateCommand(async () => await ExecuteSaveCommandAsync(), CanExecuteSaveCommand)
                                               .ObservesProperty(() => Value)
                                               .ObservesProperty(() => IsBusy));

        protected virtual bool CanExecuteSaveCommand()
        {
            return !IsBusy && Key.HasText() && Value.HasText();
        }

        protected virtual Task ExecuteSaveCommandAsync()
        {
            return SafeExecuteInvoke(() => SettingsService.UpdateSettingsByKey(Key, Value));
        }
    }
}