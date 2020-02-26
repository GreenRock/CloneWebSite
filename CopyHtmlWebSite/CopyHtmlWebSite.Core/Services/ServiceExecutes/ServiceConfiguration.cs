using System;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Services.ServiceExecutes
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        private readonly ServiceExecute _serviceExecute;
        public ServiceConfiguration(ServiceExecute serviceExecute)
        {
            _serviceExecute = serviceExecute;
        }

        public IServiceConfiguration Throw<TException>() where TException : Exception
        {
            _serviceExecute.Throw<TException>();
            return this;
        }

        public IServiceConfiguration ThrowIf<TException>(Func<TException, bool> predicate) where TException : Exception
        {
            _serviceExecute.Throw(new ServiceExecutePredicateValidator(ex => predicate.Invoke((TException)ex), ex => throw ex));
            return this;
        }

        public IServiceConfiguration Handle<TException, TResult>(Func<TException, Task<TResult>> handleException) where TException : Exception
        {
            _serviceExecute.Handle(new ServiceExecuteValidator<TResult>(typeof(TException), ex => handleException((TException)ex)));
            return this;
        }

        public IServiceConfiguration Handle<TException, TResult>(Func<TException, Task<TResult>> handleException, int order) where TException : Exception
        {
            _serviceExecute.Handle(new ServiceExecuteValidator<TResult>(typeof(TException), ex => handleException((TException)ex), order));
            return this;
        }

        public IServiceConfiguration Handle<TException>(Func<TException, Task<object>> handleException, int order) where TException : Exception
        {
            _serviceExecute.Handle(new ServiceExecuteValidator(typeof(TException), ex => handleException.Invoke((TException)ex), order));
            return this;
        }

        public IServiceConfiguration HandleIf<TException, TResult>(Func<TException, bool> predicate, Func<TException, Task<TResult>> handleException) where TException : Exception
        {
            _serviceExecute.Handle(new ServiceExecutePredicateValidator<TResult>(ex => predicate.Invoke((TException)ex), ex => handleException.Invoke((TException)ex)));
            return this;
        }

        public IServiceConfiguration HandleIf<TException, TResult>(Func<TException, bool> predicate, Func<TException, Task<TResult>> handleException, int order) where TException : Exception
        {
            _serviceExecute.Handle(new ServiceExecutePredicateValidator<TResult>(ex => predicate.Invoke((TException)ex), ex => handleException.Invoke((TException)ex), order));
            return this;
        }
    }
}