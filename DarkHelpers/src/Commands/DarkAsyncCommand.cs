using DarkHelpers.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarkHelpers.Commands
{
    /// <summary>
    /// Implementation of an Async Command
    /// </summary>
    public class DarkAsyncCommand : IDarkAsyncCommand
	{
		readonly Func<Task> execute;
		readonly Func<object, bool>? canExecute;
		readonly Action<Exception>? onException;
		readonly bool continueOnCapturedContext;
		readonly DarkEventManager weakEventManager = new DarkEventManager();

		/// <summary>
		/// Create a new AsyncCommand
		/// </summary>
		/// <param name="execute">Function to execute</param>
		/// <param name="canExecute">Function to call to determine if it can be executed</param>
		/// <param name="onException">Action callback when an exception occurs</param>
		/// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
		public DarkAsyncCommand(Func<Task> execute,
							Func<object, bool>? canExecute = null,
							Action<Exception>? onException = null,
							bool continueOnCapturedContext = false)
		{
			this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
			this.canExecute = canExecute;
			this.onException = onException;
			this.continueOnCapturedContext = continueOnCapturedContext;
		}

		/// <summary>
		/// Event triggered when Can Excecute changes.
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add { weakEventManager.AddEventHandler(value); }
			remove { weakEventManager.RemoveEventHandler(value); }
		}

		/// <summary>
		/// Invoke the CanExecute method and return if it can be executed.
		/// </summary>
		/// <param name="parameter">Parameter to pass to CanExecute.</param>
		/// <returns>If it can be executed.</returns>
		public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task of action being executed that can be awaited.</returns>
		public Task ExecuteAsync() => execute();

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		public void RaiseCanExecuteChanged() => weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

		#region Explicit implementations
		void ICommand.Execute(object parameter) => ExecuteAsync().SafeFireAndForget(onException, continueOnCapturedContext);
		#endregion
	}

	/// <summary>
	/// Implementation of a generic Async Command
	/// </summary>
	public class DarkAsyncCommand<T> : IDarkAsyncCommand<T>
	{

		readonly Func<T, Task> execute;
		readonly Func<object, bool>? canExecute;
		readonly Action<Exception>? onException;
		readonly bool continueOnCapturedContext;
		readonly DarkEventManager weakEventManager = new DarkEventManager();

		/// <summary>
		/// Create a new AsyncCommand
		/// </summary>
		/// <param name="execute">Function to execute</param>
		/// <param name="canExecute">Function to call to determine if it can be executed</param>
		/// <param name="onException">Action callback when an exception occurs</param>
		/// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
		public DarkAsyncCommand(Func<T, Task> execute,
							Func<object, bool>? canExecute = null,
							Action<Exception>? onException = null,
							bool continueOnCapturedContext = false)
		{
			this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
			this.canExecute = canExecute;
			this.onException = onException;
			this.continueOnCapturedContext = continueOnCapturedContext;
		}

		/// <summary>
		/// Event triggered when Can Excecute changes.
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add { weakEventManager.AddEventHandler(value); }
			remove { weakEventManager.RemoveEventHandler(value); }
		}

		/// <summary>
		/// Invoke the CanExecute method and return if it can be executed.
		/// </summary>
		/// <param name="parameter">Parameter to pass to CanExecute.</param>
		/// <returns>If it can be executed</returns>
		public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task that is executing and can be awaited.</returns>
		public Task ExecuteAsync(T parameter) => execute(parameter);

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		public void RaiseCanExecuteChanged() => weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

		#region Explicit implementations

		void ICommand.Execute(object parameter)
		{
			if (CommandUtils.IsValidCommandParameter<T>(parameter))
				ExecuteAsync((T)parameter).SafeFireAndForget(onException, continueOnCapturedContext);

		}
		#endregion
	}
}
