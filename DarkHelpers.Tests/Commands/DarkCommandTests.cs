using DarkHelpers.Commands;
using DarkHelpers.Exceptions;
using System;
using Xunit;

namespace DarkHelpers.Tests.Commands
{
    public class DarkCommandTests
    {
		[Fact]
		public void Constructor()
		{
			var cmd = new DarkCommand(() => { });
			Assert.True(cmd.CanExecute(null));
		}

		[Fact]
		public void ThrowsWithNullConstructor()
		{
			Assert.Throws<ArgumentNullException>(() => new DarkCommand((Action)null));
		}

		[Fact]
		public void ThrowsWithNullParameterizedConstructor()
		{
			Assert.Throws<ArgumentNullException>(() => new DarkCommand((Action<object>)null));
		}

		[Fact]
		public void ThrowsWithNullExecuteValidCanExecute()
		{
			Assert.Throws<ArgumentNullException>(() => new DarkCommand(null, () => true));
		}

		[Fact]
		public void Execute()
		{
			var executed = false;
			var cmd = new DarkCommand(() => executed = true);

			cmd.Execute(null);
			Assert.True(executed);
		}

		[Fact]
		public void ExecuteParameterized()
		{
			object executed = null;
			var cmd = new DarkCommand(o => executed = o);

			var expected = new object();
			cmd.Execute(expected);

			Assert.Equal(expected, executed);
		}

		[Fact]
		public void ExecuteWithCanExecute()
		{
			var executed = false;
			var cmd = new DarkCommand(() => executed = true, () => true);

			cmd.Execute(null);
			Assert.True(executed);
		}

		[Fact]
		public void CanExecute()
		{
			var canExecuteRan = false;
			var cmd = new DarkCommand(() => { }, () =>
			{
				canExecuteRan = true;
				return true;
			});

			Assert.True(cmd.CanExecute(null));
			Assert.True(canExecuteRan);
		}

		[Fact]
		public void ChangeCanExecute()
		{
			var signaled = false;
			var cmd = new DarkCommand(() => { });

			cmd.CanExecuteChanged += (sender, args) => signaled = true;

			cmd.RaiseCanExecuteChanged();
			Assert.True(signaled);
		}

		[Fact]
		public void GenericThrowsWithNullExecute()
		{
			Assert.Throws<ArgumentNullException>(() => new DarkCommand<string>(null));
		}

		[Fact]
		public void GenericThrowsWithNullExecuteAndCanExecuteValid()
		{
			Assert.Throws<ArgumentNullException>(() => new DarkCommand<string>(null, s => true));
		}

		[Fact]
		public void GenericThrowsWithValidExecuteAndCanExecuteNull()
		{
			Assert.Throws<ArgumentNullException>(() => new DarkCommand<string>(s => { }, null));
		}

		[Fact]
		public void GenericExecute()
		{
			string result = null;
			var cmd = new DarkCommand<string>(s => result = s);

			cmd.Execute("Foo");
			Assert.Equal("Foo", result);
		}

		[Fact]
		public void GenericExecuteWithCanExecute()
		{
			string result = null;
			var cmd = new DarkCommand<string>(s => result = s, s => true);

			cmd.Execute("Foo");
			Assert.Equal("Foo", result);
		}

		[Fact]
		public void GenericCanExecute()
		{
			string result = null;
			var cmd = new DarkCommand<string>(s => { }, s =>
			{
				result = s;
				return true;
			});

			Assert.True(cmd.CanExecute("Foo"));
			Assert.Equal("Foo", result);
		}

		class FakeParentContext
		{
		}

		class FakeChildContext
		{
		}

		[Fact]
		public void CanExecuteReturnsFalseIfParameterIsWrongReferenceType()
		{
			var command = new DarkCommand<FakeChildContext>(context => { }, context => true);

			Assert.Throws<InvalidCommandParameterException>(() => command.CanExecute(new FakeParentContext()));
		}

		[Fact]
		public void CanExecuteReturnsFalseIfParameterIsWrongValueType()
		{
			var command = new DarkCommand<int>(context => { }, context => true);

			Assert.Throws<InvalidCommandParameterException>(() => command.CanExecute(10.5));
		}

		[Fact]
		public void CanExecuteUsesParameterIfReferenceTypeAndSetToNull()
		{
			var command = new DarkCommand<FakeChildContext>(context => { }, context => true);

			Assert.True(command.CanExecute(null), "null is a valid value for a reference type");
		}

		[Fact]
		public void CanExecuteUsesParameterIfNullableAndSetToNull()
		{
			var command = new DarkCommand<int?>(context => { }, context => true);

			Assert.True(command.CanExecute(null), "null is a valid value for a Nullable<int> type");
		}

		[Fact]
		public void CanExecuteIgnoresParameterIfValueTypeAndSetToNull()
		{
			var command = new DarkCommand<int>(context => { }, context => true);

			Assert.Throws<InvalidCommandParameterException>(() => command.CanExecute(null));
		}

		[Fact]
		public void ExecuteDoesNotRunIfParameterIsWrongReferenceType()
		{
			var executions = 0;
			var command = new DarkCommand<FakeChildContext>(context => executions += 1);

			Assert.True(executions == 0, "the command should not have executed");
		}

		[Fact]
		public void ExecuteDoesNotRunIfParameterIsWrongValueType()
		{
			var executions = 0;
			var command = new DarkCommand<int>(context => executions += 1);

			Assert.True(executions == 0, "the command should not have executed");
		}

		[Fact]
		public void ExecuteRunsIfReferenceTypeAndSetToNull()
		{
			var executions = 0;
			var command = new DarkCommand<FakeChildContext>(context => executions += 1);
			command.Execute(null);
			Assert.True(executions == 1, "the command should have executed");
		}

		[Fact]
		public void ExecuteRunsIfNullableAndSetToNull()
		{
			var executions = 0;
			var command = new DarkCommand<int?>(context => executions += 1);
			command.Execute(null);
			Assert.True(executions == 1, "the command should have executed");
		}

		[Fact]
		public void ExecuteDoesNotRunIfValueTypeAndSetToNull()
		{
			var executions = 0;
			var command = new DarkCommand<int>(context => executions += 1);

			Assert.True(executions == 0, "the command should not have executed");
		}
	}
}
