using System.ComponentModel;
using Xunit;

namespace DarkHelpers.Tests
{
    public class DarkViewModelTests
    {
		[Fact]
		public void CanLoadMore()
		{
			PropertyChangedEventArgs updated = null;
			var vm = new DarkViewModel();

			vm.PropertyChanged += (sender, args) =>
			{
				updated = args;
			};

			vm.CanLoadMore = false;
			Assert.NotNull(updated);
			Assert.Equal(nameof(vm.CanLoadMore), updated.PropertyName);
		}

		[Fact]
		public void IsLoadingMore()
		{
			PropertyChangedEventArgs updated = null;
			var vm = new DarkViewModel();

			vm.PropertyChanged += (sender, args) =>
			{
				updated = args;
			};

			vm.IsLoadingMore = false;
			Assert.NotNull(updated);
			Assert.Equal(nameof(vm.IsLoadingMore), updated.PropertyName);
		}

		[Fact]
		public void IsBusy()
		{
			PropertyChangedEventArgs updated = null;
			var vm = new DarkViewModel();

			vm.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == "IsBusy")
                {
                    updated = args;
                }
            };

			vm.IsBusy = true;
			Assert.NotNull(updated);
			Assert.Equal(nameof(vm.IsBusy), updated.PropertyName);
			Assert.False(vm.IsNotBusy);
		}

		[Fact]
		public void IsNotBusy()
		{
			PropertyChangedEventArgs updated = null;
			var vm = new DarkViewModel();

			vm.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == "IsNotBusy")
                {
                    updated = args;
                }
            };

			vm.IsNotBusy = false;

			Assert.NotNull(updated);
			Assert.Equal(nameof(vm.IsNotBusy), updated.PropertyName);
			Assert.True(vm.IsBusy);
		}
	}
}
