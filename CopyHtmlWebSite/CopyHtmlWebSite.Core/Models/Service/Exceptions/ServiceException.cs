using System;

namespace CopyHtmlWebSite.Core.Models.Service.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string message) : base(message)
        {
            
        }

        public ServiceException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
