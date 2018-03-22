using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Download.Common.Extensions
{
    public class WebClientExtension
    {
        public  event DownloadProgressChangedEventHandler ProgressChanged;
        public  event DownloadStringCompletedEventHandler Completed;

        public  async Task<T> WebClientSetup<T>(Func<WebClient, Task<T>> action)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    webClient.DownloadStringCompleted += Completed;
                    webClient.DownloadProgressChanged += ProgressChanged;

                    return await action.Invoke(webClient);
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                return default(T);
            }
        }

        public  async Task<string> DownloadString(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            return await WebClientSetup(webClient => webClient.DownloadStringTaskAsync(new Uri(url)));
        }

        public  async Task DownloadFile(string url, string directory)
        {
            if (string.IsNullOrEmpty(url))
                return;

            await WebClientSetup(webClient => (Task<Task>)webClient.DownloadFileTaskAsync(new Uri(url), directory));
        }

        public  bool IsUrl(string url)
        {
            try
            {
                Uri uri;
                if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                    return false;

                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "HEAD";

                using (var httpWebResponse = request.GetResponse())
                using (var response = (HttpWebResponse)httpWebResponse)
                {
                    var statusCode = response.StatusCode;
                    httpWebResponse.Close();
                    return statusCode == HttpStatusCode.OK && !response.ContentType.Contains("text/html");
                }
            }
            catch (WebException)
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
