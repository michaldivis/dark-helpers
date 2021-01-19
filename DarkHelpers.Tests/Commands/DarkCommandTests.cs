using DarkHelpers.Commands;
using DarkHelpers.Exceptions;
using NUnit.Framework;
using System;

namespace DarkHelpers.Tests.Commands
{
    public class DarkCommandTests
    {
		[Test]
		public void Constructor()
		{
			var cmd = new DarkCommand(() => { });
			Assert.IsTrue(cmd.CanExecute(null));
		}

		[Test]
		public void ThrowsWithNullConstructor()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new DarkCommand((Action)null));
		}

		[Test]
		public void ThrowsWithNullParameterizedConstructor()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new DarkCommand((Action<object>)null));
		}

		[Test]
		public void ThrowsWithNullExecuteValidCanExecute()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new DarkCommand(null, () => true));
		}

		[Test]
		public void Execute()
		{
			var executed = false;
			var cmd = new DarkCommand(() => executed = true);

			cmd.Execute(null);
			Assert.IsTrue(executed);
		}

		[Test]
		public void ExecuteParameterized()
		{
			object executed = null;
			var cmd = new DarkCommand(o => executed = o);

			var expected = new object();
			cmd.Execute(expected);

			Assert.AreEqual(expected, executed);
		}

		[Test]
		public void ExecuteWithCanExecute()
		{
			var executed = false;
			var cmd = new DarkCommand(() => executed = true, () => true);

			cmd.Execute(null);
			Assert.IsTrue(executed);
		}

		[Test]
		public void CanExecute()
		{
			var canExecuteRan = false;
			var cmd = new DarkCommand(() => { }, () =>
			{
				canExecuteRan = true;
				return true;
			});

			Assert.AreEqual(true, cmd.CanExecute(null));
			Assert.IsTrue(canExecuteRan);
		}

		[Test]
		public void ChangeCanExecute()
		{
			var signaled = false;
			var cmd = new DarkCommand(() => { });

			cmd.CanExecuteChanged += (sender, args) => signaled = true;

			cmd.RaiseCanExecuteChanged();
			Assert.IsTrue(signaled);
		}

		[Test]
		public void GenericThrowsWithNullExecute()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new DarkCommand<string>(null));
		}

		[Test]
		public void GenericThrowsWithNullExecuteAndCanExecuteValid()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new DarkCommand<string>(null, s => true));
		}

		[Test]
		public void GenericThrowsWithValidExecuteAndCanExecuteNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new DarkCommand<string>(s => { }, null));
		}

		[Test]
		public void GenericExecute()
		{
			string result = null;
			var cmd = new DarkCommand<string>(s => result = s);

			cmd.Execute("Foo");
			Assert.AreEqual("Foo", result);
		}

		[Test]
		public void GenericExecuteWithCanExecute()
		{
			string result = null;
			var cmd = new DarkCommand<string>(s => result = s, s => true);

			cmd.Execute("Foo");
			Assert.AreEqual("Foo", result);
		}

		[Test]
		public void GenericCanExecute()
		{
			string result = null;
			var cmd = new DarkCommand<string>(s => { }, s =>
			{
				result = s;
				return true;
			});

			Assert.AreEqual(true, cmd.CanExecute("Foo"));
			Assert.AreEqual("Foo", result);
		}

		class FakeParentContext
		{
		}

		class FakeChildContext
		{
		}

		[Test]
		public void CanExecuteReturnsFalseIfParameterIsWrongReferenceType()
		{
			var command = new DarkCommand<FakeChildContext>(context => { }, context => true);

			Assert.Throws(typeof(InvalidCommandParameterException), () => command.CanExecute(new FakeParentContext()));
		}

		[Test]
		public void CanExecuteReturnsFalseIfParameterIsWrongValueType()
		{
			var command = new DarkCommand<int>(context => { }, context => true);

			Assert.Throws(typeof(InvalidCommandParameterException), () => command.CanExecute(10.5));
		}

		[Test]
		public void CanExecuteUsesParameterIfReferenceTypeAndSetToNull()
		{
			var command = new DarkCommand<FakeChildContext>(context => { }, context => true);

			Assert.IsTrue(command.CanExecute(null), "null is a valid value for a reference type");
		}

		[Test]
		public void CanExecuteUsesParameterIfNullableAndSetToNull()
		{
			var command = new DarkCommand<int?>(context => { }, context => true);

			Assert.IsTrue(command.CanExecute(null), "null is a valid value for a Nullable<int> type");
		}

		[Test]
		public void CanExecuteIgnoresParameterIfValueTypeAndSetToNull()
		{
			var command = new DarkCommand<int>(context => { }, context => true);

			Assert.Throws(typeof(InvalidCommandParameterException), () => command.CanExecute(null));
		}

		[Test]
		public void ExecuteDoesNotRunIfParameterIsWrongReferenceType()
		{
			var executions = 0;
			var command = new DarkCommand<FakeChildContext>(context => executions += 1);

			Assert.IsTrue(executions == 0, "the command should not have executed");
		}

		[Test]
		public void ExecuteDoesNotRunIfParameterIsWrongValueType()
		{
			var executions = 0;
			var command = new DarkCommand<int>(context => executions += 1);

			Assert.IsTrue(executions == 0, "the command should not have executed");
		}

		[Test]
		public void ExecuteRunsIfReferenceTypeAndSetToNull()
		{
			var executions = 0;
			var command = new DarkCommand<FakeChildContext>(context => executions += 1);
			command.Execute(null);
			Assert.IsTrue(executions == 1, "the command should have executed");
		}

		[Test]
		public void ExecuteRunsIfNullableAndSetToNull()
		{
			var executions = 0;
			var command = new DarkCommand<int?>(context => executions += 1);
			command.Execute(null);
			Assert.IsTrue(executions == 1, "the command should have executed");
		}

		[Test]
		public void ExecuteDoesNotRunIfValueTypeAndSetToNull()
		{
			var executions = 0;
			var command = new DarkCommand<int>(context => executions += 1);

			Assert.IsTrue(executions == 0, "the command should not have executed");
		}
	}
}
