using System;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Services.ServiceExecutes
{
    public interface IServiceExecuteValidator
    {
        int Order { get; set; }
        bool IsValid(Exception ex);
        Func<Exception, Task<object>> Execute { get; set; }
    }
}