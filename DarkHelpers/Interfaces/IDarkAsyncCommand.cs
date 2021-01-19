using System.Threading.Tasks;
using System.Windows.Input;

namespace DarkHelpers.Interfaces
{
    /// <summary>
    /// Interface for Async Command
    /// </summary>
    public interface IDarkAsyncCommand : ICommand
	{
		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task to be awaited on.</returns>
		Task ExecuteAsync();

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		void RaiseCanExecuteChanged();
	}

	/// <summary>
	/// Interface for Async Command with parameter
	/// </summary>
	public interface IDarkAsyncCommand<T> : ICommand
	{
		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <param name="parameter">Parameter to pass to command</param>
		/// <returns>Task to be awaited on.</returns>
		Task ExecuteAsync(T parameter);

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		void RaiseCanExecuteChanged();
	}
}
