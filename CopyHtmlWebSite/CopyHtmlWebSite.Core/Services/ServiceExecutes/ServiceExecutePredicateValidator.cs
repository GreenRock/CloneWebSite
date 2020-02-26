using System;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Services.ServiceExecutes
{
    public class ServiceExecutePredicateValidator : ServiceExecutePredicateValidator<object>
    {
        public ServiceExecutePredicateValidator()
        {

        }

        public ServiceExecutePredicateValidator(Func<Exception, bool> predicate, Func<Exception, Task<object>> execute, int order = 100) : base(predicate, execute, order)
        {
        }
    }

    public class ServiceExecutePredicateValidator<TResult> : IServiceExecuteValidator
    {
        public ServiceExecutePredicateValidator()
        {

        }

        public ServiceExecutePredicateValidator(Func<Exception, bool> predicate, Func<Exception, Task<TResult>> execute, int order = 100)
        {
            Predicate = predicate;
            Execute = async ex => await execute(ex);
            Order = order;
        }

        public Func<Exception, bool> Predicate { get; set; }

        public int Order { get; set; }

        public bool IsValid(Exception ex)
        {
            return Predicate(ex);
        }

        public Func<Exception, Task<object>> Execute { get; set; }
    }
}