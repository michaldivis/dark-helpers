using DarkHelpers.Commands;
using DarkHelpers.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace DarkHelpers.Tests.Commands
{
    public class IDarkAsyncCommandTests
	{
		[Fact]
		public void IAsyncCommand_CanRaiseCanExecuteChanged()
		{
			IDarkAsyncCommand command = new DarkAsyncCommand(() => Task.CompletedTask);
			command.RaiseCanExecuteChanged();
		}

		[Fact]
		public void IAsyncCommandT_CanRaiseCanExecuteChanged()
		{
			IDarkAsyncCommand<string> command = new DarkAsyncCommand<string>(sender => Task.CompletedTask);
			command.RaiseCanExecuteChanged();
		}
	}
}
