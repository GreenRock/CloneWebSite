using System.Threading.Tasks;

namespace Download.Services.WebClientServices
{
    public interface IWebClientService
    {
        Task<string> DownloadStringAsync(string url);
        Task DownloadFileAsync(string url, string directory);
        Task<bool> IsUrl(string url);
        Task<bool> IsUrlAsync(string url);
    }
}