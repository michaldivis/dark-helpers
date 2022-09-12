using DarkHelpers.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarkHelpers
{
    /// <summary>
    /// Extension Utils
    /// </summary>
    public static class DarkExtensions
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

#pragma warning disable CS8603 // Possible null reference return.
			return retTask is Task<T> ? task.Result : default;
#pragma warning restore CS8603 // Possible null reference return.
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
		public static async void SafeFireAndForget(this Task task, Action<Exception>? onException = null, bool continueOnCapturedContext = false)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
		{
			try
			{
				await task.ConfigureAwait(continueOnCapturedContext);
			}
			catch (Exception ex) when (onException != null)
			{
				onException.Invoke(ex);
			}
		}

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
		/// <summary>
		/// Attempts to await on the task and catches exception
		/// </summary>
		/// <param name="task">Task to execute</param>
		/// <param name="errorHandler">An error handler implementation that will handle possible exceptions</param>
		/// <param name="continueOnCapturedContext">If the context should be captured.</param>
		public static async void SafeFireAndForget(this Task task, ITaskErrorHandler errorHandler = null, bool continueOnCapturedContext = false)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
		{
			try
			{
				await task.ConfigureAwait(continueOnCapturedContext);
			}
			catch (Exception ex) when (errorHandler != null)
			{
				errorHandler.Handle(ex);
			}
		}

		/// <summary>
		/// Checks if the <see cref="ICommand" /> can be executed, and if yes, executes it
		/// </summary>
		/// <param name="command"><see cref="ICommand" /> to execute</param>
		/// <param name="parameter">Parameter to pass to the <see cref="ICommand" /></param>
		public static void TryExecute(this ICommand command, object? parameter = null)
        {
			if(command is null)
            {
				return;
            }

            if (command.CanExecute(parameter))
            {
				command.Execute(parameter);
            }
        }
	}
}
