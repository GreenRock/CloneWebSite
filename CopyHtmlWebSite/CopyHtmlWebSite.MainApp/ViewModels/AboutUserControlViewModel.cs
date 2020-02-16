using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using CopyHtmlWebSite.Core.Extensions;
using Prism.Commands;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
    using Prism.Regions;
    using ViewModelBases;
    public class AboutUserControlViewModel : NavigationViewModelBase
    {
        public AboutUserControlViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Owner = Properties.Settings.Default.Owner;
            Email = Properties.Settings.Default.Email;
        }

        private string _owner;
        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private ICommand _openMailCommand;
        public ICommand OpenMailCommand => _openMailCommand ?? (_openMailCommand = new DelegateCommand<string>(ExecuteOpenMailCommand, CanExecuteOpenMailCommand)
                                        .ObservesProperty(() => IsBusy));

        private bool CanExecuteOpenMailCommand(string email)
        {
            return !IsBusy;
        }

        void ExecuteOpenMailCommand(string email)
        {
            SafeExecuteInvoke(() =>
            {
                Process.Start($"mailto:{email}");
                return Task.CompletedTask;
            });
        }
    }
}
