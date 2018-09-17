namespace CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using Prism.Mvvm;

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

        public virtual Task HandleError(Exception ex)
        {
            return Task.CompletedTask;
        }

        protected void SetWithTask(Action invoke)
        {
            Application.Current.Dispatcher.Invoke(invoke);
        }
    }
}