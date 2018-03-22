using System;
using System.Threading.Tasks;

namespace Download.Services.CommandServices
{
    public class CommandService : ICommandService
    {
        public T Execute<T>(Func<T> command, Action<Exception> commandError)
        {
            try
            {
                return command.Invoke();
            }
            catch (Exception e)
            {
                commandError.Invoke(e);
                return default(T);
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> command, Action<Exception> commandError)
        {
            try
            {
                return await command.Invoke();
            }
            catch (Exception e)
            {
                commandError.Invoke(e);
                return default(T);
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> command, Func<Exception, Task> commandError)
        {
            try
            {
                return await command.Invoke();
            }
            catch (Exception e)
            {
                await commandError(e);
                return default(T);
            }
        }
    }
}
