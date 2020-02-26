using System.Threading.Tasks;

namespace ShopMeDang.Infrastructure.Services.EmailServices
{
    public interface IEmailService
    {
        bool Send(EmailRequest request);
        Task<bool> SendAsync(EmailRequest request);
    }
}