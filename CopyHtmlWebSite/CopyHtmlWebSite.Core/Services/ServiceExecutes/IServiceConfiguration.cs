using System;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Services.ServiceExecutes
{
    public interface IServiceConfiguration
    {
        IServiceConfiguration Throw<TException>() where TException : Exception;
        IServiceConfiguration ThrowIf<TException>(Func<TException, bool> predicate) where TException : Exception;
        
        IServiceConfiguration Handle<TException, TResult>(Func<TException, Task<TResult>> handleException) where TException : Exception;
        IServiceConfiguration Handle<TException, TResult>(Func<TException, Task<TResult>> handleException, int order) where TException : Exception;

        IServiceConfiguration HandleIf<TException, TResult>(Func<TException, bool> predicate, Func<TException, Task<TResult>> handleException) where TException : Exception;
        IServiceConfiguration HandleIf<TException, TResult>(Func<TException, bool> predicate, Func<TException, Task<TResult>> handleException, int order) where TException : Exception;
    }
}