using System;
using System.Threading.Tasks;

namespace Download.Services.CommandServices
{
    public interface ICommandService
    {
        T Execute<T>(Func<T> command, Action<Exception> commandError);
        Task<T> ExecuteAsync<T>(Func<Task<T>> command, Action<Exception> commandError);
        Task<T> ExecuteAsync<T>(Func<Task<T>> command, Func<Exception, Task> commandError);
    }
}