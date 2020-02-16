using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CopyHtmlWebSite.Core.Extensions
{
	/// <summary>
	/// Extension Utils
	/// https://github.com/jamesmontemagno/mvvm-helpers/tree/master/MvvmHelpers
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Task extension to add a timeout.
		/// </summary>
		/// <returns>The task with timeout.</returns>
		/// <param name="task">Task.</param>
		/// <param name="timeoutInMilliseconds">Timeout duration in Milliseconds.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public async static Task<T> WithTimeout<T>(this Task<T> task, int timeoutInMilliseconds)
		{
			var retTask = await Task.WhenAny(task, Task.Delay(timeoutInMilliseconds))
				.ConfigureAwait(false);

			return retTask is Task<T> ? task.Result : default;
		}

		/// <summary>
		/// Task extension to add a timeout.
		/// </summary>
		/// <returns>The task with timeout.</returns>
		/// <param name="task">Task.</param>
		/// <param name="timeout">Timeout Duration.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout) =>
			WithTimeout(task, (int)timeout.TotalMilliseconds);

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
		/// <summary>
		/// Attempts to await on the task and catches exception
		/// </summary>
		/// <param name="task">Task to execute</param>
		/// <param name="onException">What to do when method has an exception</param>
		/// <param name="continueOnCapturedContext">If the context should be captured.</param>
		public static void SafeFireAndForget(this Task task, Action<Exception> onException = null, bool continueOnCapturedContext = false)
		{
            Task.Run(() => task.ContinueWith(tResult =>
                {
                    Debug.WriteLine(tResult.Exception);
                }, TaskContinuationOptions.OnlyOnFaulted))
                .ConfigureAwait(false);
		}	

	}
}

