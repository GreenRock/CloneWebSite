using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases
{
    public abstract class ViewModelBase : BindableBase
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, OnIsBusyChanged);
        }

        protected void SetBusy(bool val = true)
        {
            SetWithTask(() => { IsBusy = val; });
        }

        protected virtual void OnIsBusyChanged()
        {
        }

        protected Task SafeExecuteInvoke(Func<Task> execute)
        {
            try
            {
                SetBusy();
                return execute();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
            finally
            {
                ClearBusy();
            }
        }

        protected void ClearBusy()
        {
            SetBusy(false);
        }

        protected virtual Task HandleError(Exception ex)
        {
            Debug.WriteLine(ex);
            return Task.CompletedTask;
        }

        protected void SetWithTask(Action invoke)
        {
            Application.Current.Dispatcher?.Invoke(invoke);
        }
    }
}