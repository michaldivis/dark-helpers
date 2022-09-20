using DarkHelpers.Abstractions;
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
		readonly Func<Task> _execute;
		readonly Func<object?, bool>? _canExecute;
		readonly Action<Exception>? _onException;
		readonly bool _continueOnCapturedContext;
		readonly DarkEventManager _weakEventManager = new();

		/// <summary>
		/// Create a new AsyncCommand
		/// </summary>
		/// <param name="execute">Function to execute</param>
		/// <param name="canExecute">Function to call to determine if it can be executed</param>
		/// <param name="onException">Action callback when an exception occurs</param>
		/// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
		public DarkAsyncCommand(Func<Task> execute,
							Func<object?, bool>? canExecute = null,
							Action<Exception>? onException = null,
							bool continueOnCapturedContext = false)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
			_onException = onException;
			_continueOnCapturedContext = continueOnCapturedContext;
		}

		/// <summary>
		/// Event triggered when Can Excecute changes.
		/// </summary>
		public event EventHandler? CanExecuteChanged
		{
			add { _weakEventManager.AddEventHandler(value); }
			remove { _weakEventManager.RemoveEventHandler(value); }
		}

		/// <summary>
		/// Invoke the CanExecute method and return if it can be executed.
		/// </summary>
		/// <param name="parameter">Parameter to pass to CanExecute.</param>
		/// <returns>If it can be executed.</returns>
		public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task of action being executed that can be awaited.</returns>
		public Task ExecuteAsync() => _execute();

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		public void RaiseCanExecuteChanged() => _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

		#region Explicit implementations
		void ICommand.Execute(object? parameter) => ExecuteAsync().SafeFireAndForget(_onException, _continueOnCapturedContext);
		#endregion
	}

	/// <summary>
	/// Implementation of a generic Async Command
	/// </summary>
	public class DarkAsyncCommand<T> : IDarkAsyncCommand<T>
	{

		readonly Func<T?, Task> _execute;
		readonly Func<object?, bool>? _canExecute;
		readonly Action<Exception>? _onException;
		readonly bool _continueOnCapturedContext;
		readonly DarkEventManager _weakEventManager = new();

		/// <summary>
		/// Create a new AsyncCommand
		/// </summary>
		/// <param name="execute">Function to execute</param>
		/// <param name="canExecute">Function to call to determine if it can be executed</param>
		/// <param name="onException">Action callback when an exception occurs</param>
		/// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
		public DarkAsyncCommand(Func<T?, Task> execute,
							Func<object?, bool>? canExecute = null,
							Action<Exception>? onException = null,
							bool continueOnCapturedContext = false)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
			_onException = onException;
			_continueOnCapturedContext = continueOnCapturedContext;
		}

		/// <summary>
		/// Event triggered when Can Excecute changes.
		/// </summary>
		public event EventHandler? CanExecuteChanged
		{
			add { _weakEventManager.AddEventHandler(value); }
			remove { _weakEventManager.RemoveEventHandler(value); }
		}

		/// <summary>
		/// Invoke the CanExecute method and return if it can be executed.
		/// </summary>
		/// <param name="parameter">Parameter to pass to CanExecute.</param>
		/// <returns>If it can be executed</returns>
		public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task that is executing and can be awaited.</returns>
		public Task ExecuteAsync(T? parameter) => _execute(parameter);

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		public void RaiseCanExecuteChanged() => _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

		#region Explicit implementations

		void ICommand.Execute(object? parameter)
		{
			if (!CommandUtils.IsValidCommandParameter<T>(parameter))
			{
				return;
            }

            ExecuteAsync((T?)parameter).SafeFireAndForget(_onException, _continueOnCapturedContext);
        }
		#endregion
	}
}
