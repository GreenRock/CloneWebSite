using System.Threading.Tasks;

namespace Download.Services.WebClientServices
{
    public interface IWebClientService
    {
        Task<string> DownloadStringAsync(string url, string referer);
        Task DownloadFileAsync(string url, string directory, string referer);
        Task<bool> IsUrl(string url, string referer);
        Task<bool> IsUrlAsync(string url);
    }
}