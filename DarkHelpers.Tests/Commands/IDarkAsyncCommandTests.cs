using DarkHelpers.Commands;
using DarkHelpers.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DarkHelpers.Tests.Commands
{
    public class IDarkAsyncCommandTests
	{
		[Test]
		public void IAsyncCommand_CanRaiseCanExecuteChanged()
		{
			IDarkAsyncCommand command = new DarkAsyncCommand(() => Task.CompletedTask);
			command.RaiseCanExecuteChanged();
		}

		[Test]
		public void IAsyncCommandT_CanRaiseCanExecuteChanged()
		{
			IDarkAsyncCommand<string> command = new DarkAsyncCommand<string>(sender => Task.CompletedTask);
			command.RaiseCanExecuteChanged();
		}
	}
}
