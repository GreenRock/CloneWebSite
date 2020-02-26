using System;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.MainApp.Services
{
    public abstract class ServiceBase
    {
        protected async Task<TResult> SafeInvoke<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                return default(TResult);
            }
        }
    }
}