using System.Diagnostics;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Extensions
{
    public static class TaskExtension
    {
        public static void RunFireAndForget(this Task task)
        {
            Task.Run(async () => await task).ContinueWith(x =>
            {
                Debug.WriteLine(x.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);
        }

        public static async Task<T> WithTimeout<T>(this Task<T> task, int timeoutInMilliseconds)
        {
            var retTask = await Task.WhenAny(task, Task.Delay(timeoutInMilliseconds))
                .ConfigureAwait(false);

            return retTask is Task<T> ? task.Result : default;
        }
    }
}
