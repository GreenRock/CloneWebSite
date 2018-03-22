using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Download.Services.WebClientServices
{
    using System.IO;
    using System.Net.Http.Headers;

    public class WebClientService : IWebClientService
    {
        public  event DownloadProgressChangedEventHandler ProgressChanged;
        public  event DownloadStringCompletedEventHandler Completed;

        public  async Task<T> WebClientSetupAsync<T>(Func<HttpClient, Task<T>> action)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063");

                    return await action.Invoke(httpClient);
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                return default(T);
            }
        }

        public  async Task<string> DownloadStringAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            return await WebClientSetupAsync(async webClient => await webClient.GetStringAsync(new Uri(url)));
        }

        public  async Task DownloadFileAsync(string url, string directory)
        {
            if (string.IsNullOrEmpty(url))
                return;
            await WebClientSetupAsync(async httpClient =>
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    var httpResponseMessage = await httpClient.SendAsync(request);
                    // Check that response was successful or throw exception
                    httpResponseMessage.EnsureSuccessStatusCode();

                    using (FileStream fileStream = new FileStream(directory, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        //copy the content from response to filestream
                        await httpResponseMessage.Content.CopyToAsync(fileStream);
                    }
                }
                return true;
            });
        }

        public  async Task<bool> IsUrl(string url)
        {
            try
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                    return false;

                return await WebClientSetupAsync(async httpClient =>
                {
                    var sendAsync = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
                    return sendAsync.IsSuccessStatusCode;
                });
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        public  async Task<bool> IsUrlAsync(string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                return false;

            using (var client = new HttpClient())
            {
                var httpRequestMsg = new HttpRequestMessage(HttpMethod.Head, uri);
                var response = await client.SendAsync(httpRequestMsg);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
