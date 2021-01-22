using DarkHelpers.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xunit;

namespace DarkHelpers.Tests.Commands
{
    public class DarkAsyncCommandTests
    {
        #region Properties

        protected const int Delay = 500;
        protected DarkEventManager TestDarkEventManager { get; } = new DarkEventManager();

        #endregion

        #region Events

        protected event EventHandler TestEvent
        {
            add => TestDarkEventManager.AddEventHandler(value);
            remove => TestDarkEventManager.RemoveEventHandler(value);
        }

        #endregion

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DarkAsyncCommand<bool>((t) => ExecuteLoadCommand(true)));

        async Task ExecuteLoadCommand(bool forceRefresh)
        {
            await Task.Delay(1000);
        }

        #region Methods
        protected Task NoParameterTask()
        {
            return Task.Delay(Delay);
        }

        protected Task IntParameterTask(int delay)
        {
            return Task.Delay(delay);
        }

        protected Task StringParameterTask(string text)
        {
            return Task.Delay(Delay);
        }

        protected Task NoParameterImmediateNullReferenceExceptionTask()
        {
            throw new NullReferenceException();
        }

        protected Task ParameterImmediateNullReferenceExceptionTask(int delay)
        {
            throw new NullReferenceException();
        }

        protected async Task NoParameterDelayedNullReferenceExceptionTask()
        {
            await Task.Delay(Delay);
            throw new NullReferenceException();
        }

        protected async Task IntParameterDelayedNullReferenceExceptionTask(int delay)
        {
            await Task.Delay(delay);
            throw new NullReferenceException();
        }

        protected bool CanExecuteTrue(object parameter)
        {
            return true;
        }

        protected bool CanExecuteFalse(object parameter)
        {
            return false;
        }

        protected bool CanExecuteDynamic(object booleanParameter)
        {
            return (bool)booleanParameter;
        }

        #endregion

        [Fact]
		public void AsyncCommand_UsingICommand()
		{
			//Arrange
			RefreshCommand.Execute(true);
		}

		[Fact]
		public void AsyncCommand_NullExecuteParameter()
		{
			//Arrange

			//Act

			//Assert
			Assert.Throws<ArgumentNullException>(() => new DarkAsyncCommand(null));
		}

		[Fact]
		public void AsyncCommandT_NullExecuteParameter()
		{
			//Arrange

			//Act

			//Assert
			Assert.Throws<ArgumentNullException>(() => new DarkAsyncCommand<object>(null));
		}

		[Fact]
		public async Task AsyncCommand_ExecuteAsync_IntParameter_Test()
		{
			//Arrange
			var command = new DarkAsyncCommand<int>(IntParameterTask);

			//Act
			await command.ExecuteAsync(500);
			await command.ExecuteAsync(default);

			//Assert
		}

		[Fact]
		public async Task AsyncCommand_ExecuteAsync_StringParameter_Test()
		{
			//Arrange
			var command = new DarkAsyncCommand<string>(StringParameterTask);

			//Act
			await command.ExecuteAsync("Hello");
			await command.ExecuteAsync(default);

			//Assert

		}

		[Fact]
		public void AsyncCommand_Parameter_CanExecuteTrue_Test()
		{
			//Arrange
			var command = new DarkAsyncCommand<int>(IntParameterTask, CanExecuteTrue);

			//Act

			//Assert
			Assert.True(command.CanExecute(null));
		}

		[Fact]
		public void AsyncCommand_Parameter_CanExecuteFalse_Test()
		{
			//Arrange
			var command = new DarkAsyncCommand<int>(IntParameterTask, CanExecuteFalse);

			//Act

			//Assert
			Assert.False(command.CanExecute(null));
		}

		[Fact]
		public void AsyncCommand_NoParameter_CanExecuteTrue_Test()
		{
			//Arrange
			var command = new DarkAsyncCommand(NoParameterTask, CanExecuteTrue);

			//Act

			//Assert
			Assert.True(command.CanExecute(null));
		}

		[Fact]
		public void AsyncCommand_NoParameter_CanExecuteFalse_Test()
		{
			//Arrange
			var command = new DarkAsyncCommand(NoParameterTask, CanExecuteFalse);

			//Act

			//Assert
			Assert.False(command.CanExecute(null));
		}


		[Fact]
		public void AsyncCommand_CanExecuteChanged_Test()
		{
			//Arrange
			var canCommandExecute = false;
			var didCanExecuteChangeFire = false;

			var command = new DarkAsyncCommand(NoParameterTask, commandCanExecute);
			command.CanExecuteChanged += handleCanExecuteChanged;

            void handleCanExecuteChanged(object sender, EventArgs e)
            {
                didCanExecuteChangeFire = true;
            }

            bool commandCanExecute(object parameter)
            {
                return canCommandExecute;
            }

            Assert.False(command.CanExecute(null));

			//Act
			canCommandExecute = true;

			//Assert
			Assert.True(command.CanExecute(null));
			Assert.False(didCanExecuteChangeFire);

			//Act
			command.RaiseCanExecuteChanged();

			//Assert
			Assert.True(didCanExecuteChangeFire);
			Assert.True(command.CanExecute(null));
		}
	}
}
