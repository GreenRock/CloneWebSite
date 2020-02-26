using System;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Services.ServiceExecutes
{
    public class ServiceExecuteValidator : ServiceExecuteValidator<object>
    {
        public ServiceExecuteValidator()
        {

        }

        public ServiceExecuteValidator(Type exceptionType, Func<Exception, Task<object>> execute, int order = 100) : base(exceptionType, execute, order)
        {
        }
    }

    public class ServiceExecuteValidator<TResult> : IServiceExecuteValidator
    {
        public ServiceExecuteValidator()
        {

        }

        public ServiceExecuteValidator(Type exceptionType, Func<Exception, Task<TResult>> execute, int order = 100)
        {
            ExceptionType = exceptionType;
            Execute = async ex => await execute(ex);
            Order = order;
        }

        public int Order { get; set; }

        public Type ExceptionType { get; set; }

        public Func<Exception, Task<object>> Execute { get; set; }

        public virtual bool IsValid(Exception ex)
        {
            return ex.GetType() == ExceptionType;
        }
    }
}