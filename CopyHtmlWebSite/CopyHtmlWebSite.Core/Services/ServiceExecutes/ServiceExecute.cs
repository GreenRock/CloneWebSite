using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Services.ServiceExecutes
{
    /// <summary>
    /// throw the exception if do not handle the exception
    /// </summary>
    public class ServiceExecute
    {
        private readonly List<IServiceExecuteValidator> _exceptionList;
        private ServiceExecute()
        {
            _exceptionList = new List<IServiceExecuteValidator>();
        }

        private static readonly Lazy<ServiceExecute> InstanceLazy = new Lazy<ServiceExecute>(() => new ServiceExecute());
        public static ServiceExecute Instance => InstanceLazy.Value;

        public static Task RunAsync(Func<Task> func)
        {
            return Instance.Execute(func);
        }

        public static Task<TResult> RunAsync<TResult>(Func<Task<TResult>> func)
        {
            return Instance.Execute(func);
        }

        public ServiceExecute Handle(IServiceExecuteValidator serviceExecuteValidator)
        {
            _exceptionList.Add(serviceExecuteValidator);
            return this;
        }

        public ServiceExecute Throw(IServiceExecuteValidator serviceExecuteValidator)
        {
            _exceptionList.Add(serviceExecuteValidator);
            return this;
        }

        private Action<Exception> _log;
        public ServiceExecute LogError(Action<Exception> log)
        {
            _log = log;
            return this;
        }

        public Task Execute(Func<Task> func, List<IServiceExecuteValidator> executeValidators = null)
        {
            return Execute<object>(async () =>
            {
                await func();
                return null;
            }, executeValidators);
        }

        public async Task<TResult> Execute<TResult>(Func<Task<TResult>> func, IEnumerable<IServiceExecuteValidator> executeValidators = null)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception e)
            {
                _log?.Invoke(e);
                var handleExceptionList = GetExecuteValidators(executeValidators);
                var handle = handleExceptionList.FirstOrDefault(x => x.IsValid(e));
                if (handle != null)
                {
                    return (TResult)await handle.Execute(e);
                }
                throw;
            }
        }

        private List<IServiceExecuteValidator> GetExecuteValidators(IEnumerable<IServiceExecuteValidator> executeValidators)
        {
            var lst = new List<IServiceExecuteValidator>();

            foreach (var item in _exceptionList)
            {
                lst.Add(item);
            }

            if (executeValidators != null)
            {
                foreach (var item in executeValidators)
                {
                    lst.Add(item);
                }
            }
            return lst;
        }

        public void Configuration(Action<IServiceConfiguration> configuration)
        {
            var config = new ServiceConfiguration(this);
            configuration.Invoke(config);
        }
    }

    public static class ServiceExecuteExtension
    {
        public static ServiceExecute Handle<TException>(this ServiceExecute execute, Func<TException, Task<object>> handleException) where TException : Exception
        {
            execute.Handle(new ServiceExecuteValidator(typeof(TException), exception => handleException.Invoke((TException)exception)));
            return execute;
        }

        public static ServiceExecute Throw<TException>(this ServiceExecute execute) where TException : Exception
        {
            execute.Throw(new ServiceExecuteValidator(typeof(TException), ex => throw ex));
            return execute;
        }

        public static ServiceExecute Default(this ServiceExecute execute, object result = default(object))
        {
            execute.Handle(new ServiceExecuteValidator(typeof(Exception), ex => Task.FromResult(result)));
            return execute;
        }

    }
}