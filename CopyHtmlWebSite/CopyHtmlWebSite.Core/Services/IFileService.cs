using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Services
{
    public interface IFileService
    {
        string GetUrl(string filePath);
        Task<bool> Delete(string filePath);
    }
}