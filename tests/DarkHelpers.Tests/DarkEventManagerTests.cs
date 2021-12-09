using System;
using Xunit;

namespace DarkHelpers.Tests
{
    public class DarkEventManagerTests
    {
		static int count;

		static void Handler(object sender, EventArgs eventArgs)
		{
			count++;
		}

		internal class TestSource
		{
			public int Count = 0;
			public TestEventSource EventSource { get; set; }
			public TestSource()
			{
				EventSource = new TestEventSource();
				EventSource.TestEvent += EventSource_TestEvent;
			}
			public void Clean() => EventSource.TestEvent -= EventSource_TestEvent;

			public void Fire() => EventSource.FireTestEvent();

			void EventSource_TestEvent(object sender, EventArgs e) => Count++;
		}

		internal class TestEventSource
		{
			readonly DarkEventManager DarkEventManager;

			public TestEventSource() => DarkEventManager = new DarkEventManager();

			public void FireTestEvent() => OnTestEvent();

			internal event EventHandler TestEvent
			{
				add { DarkEventManager.AddEventHandler(value); }
				remove { DarkEventManager.RemoveEventHandler(value); }
			}

			void OnTestEvent() => DarkEventManager.HandleEvent(this, EventArgs.Empty, nameof(TestEvent));
		}

		internal class TestSubscriber
		{
			public void Subscribe(TestEventSource source) => source.TestEvent += SourceOnTestEvent;

			void SourceOnTestEvent(object sender, EventArgs eventArgs) => Assert.False(true); //fail test
		}

		[Fact]
		public void AddHandlerWithEmptyEventNameThrowsException()
		{
			var dem = new DarkEventManager();
			Assert.Throws<ArgumentNullException>(() => dem.AddEventHandler((sender, args) => { }, ""));
		}

		[Fact]
		public void AddHandlerWithNullEventHandlerThrowsException()
		{
			var dem = new DarkEventManager();
			Assert.Throws<ArgumentNullException>(() => dem.AddEventHandler(null, "test"));
		}

		[Fact]
		public void AddHandlerWithNullEventNameThrowsException()
		{
			var dem = new DarkEventManager();
			Assert.Throws<ArgumentNullException>(() => dem.AddEventHandler((sender, args) => { }, null));
		}

		[Fact]
		public void CanRemoveEventHandler()
		{
			var source = new TestSource();
			_ = source.Count;
			source.Fire();

			Assert.True(source.Count == 1);
			source.Clean();
			source.Fire();
			Assert.True(source.Count == 1);
		}

		[Fact]
		public void CanRemoveStaticEventHandler()
		{
			var beforeRun = count;

			var source = new TestEventSource();
			source.TestEvent += Handler;
			source.TestEvent -= Handler;

			source.FireTestEvent();

			Assert.True(count == beforeRun);
		}

		[Fact]
		public void EventHandlerCalled()
		{
			var called = false;

			var source = new TestEventSource();
			source.TestEvent += (sender, args) => { called = true; };

			source.FireTestEvent();

			Assert.True(called);
		}

		[Fact]
		public void FiringEventWithoutHandlerShouldNotThrow()
		{
			var source = new TestEventSource();
			source.FireTestEvent();
		}

		[Fact]
		public void MultipleHandlersCalled()
		{
			var called1 = false;
			var called2 = false;

			var source = new TestEventSource();
			source.TestEvent += (sender, args) => { called1 = true; };
			source.TestEvent += (sender, args) => { called2 = true; };
			source.FireTestEvent();

			Assert.True(called1 && called2);
		}

		[Fact]
		public void RemoveHandlerWithEmptyEventNameThrowsException()
		{
			var dem = new DarkEventManager();
			Assert.Throws<ArgumentNullException>(() => dem.RemoveEventHandler((sender, args) => { }, ""));
		}

		[Fact]
		public void RemoveHandlerWithNullEventHandlerThrowsException()
		{
			var dem = new DarkEventManager();
			Assert.Throws<ArgumentNullException>(() => dem.RemoveEventHandler(null, "test"));
		}

		[Fact]
		public void RemoveHandlerWithNullEventNameThrowsException()
		{
			var dem = new DarkEventManager();
			Assert.Throws<ArgumentNullException>(() => dem.RemoveEventHandler((sender, args) => { }, null));
		}

		[Fact]
		public void RemovingNonExistentHandlersShouldNotThrow()
		{
			var dem = new DarkEventManager();
			dem.RemoveEventHandler((sender, args) => { }, "fake");
			dem.RemoveEventHandler(Handler, "alsofake");
		}

		[Fact]
		public void RemoveHandlerWithMultipleSubscriptionsRemovesOne()
		{
			var beforeRun = count;

			var source = new TestEventSource();
			source.TestEvent += Handler;
			source.TestEvent += Handler;
			source.TestEvent -= Handler;

			source.FireTestEvent();

			Assert.Equal(beforeRun + 1, count);
		}

		[Fact]
		public void StaticHandlerShouldRun()
		{
			var beforeRun = count;

			var source = new TestEventSource();
			source.TestEvent += Handler;

			source.FireTestEvent();

			Assert.True(count > beforeRun);
		}

		[Fact]
		public void VerifySubscriberCanBeCollected()
		{
			WeakReference wr = null;
			var source = new TestEventSource();
			new Action(() =>
			{
				var ts = new TestSubscriber();
				wr = new WeakReference(ts);
				ts.Subscribe(source);
			})();

			GC.Collect();
			GC.WaitForPendingFinalizers();

			Assert.NotNull(wr);
			Assert.False(wr.IsAlive);

			// The handler for this calls Assert.Fail, so if the subscriber has not been collected
			// the handler will be called and the test will fail
			source.FireTestEvent();
		}
	}
}
