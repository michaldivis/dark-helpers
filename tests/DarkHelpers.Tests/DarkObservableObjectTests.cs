using DarkHelpers.Tests.TestModels;
using System;
using System.ComponentModel;
using Xunit;

namespace DarkHelpers.Tests
{
    public class DarkObservableObjectTests
	{
		Item _item;

		public DarkObservableObjectTests()
		{
            _item = new Item
            {
                Name = "Cheese",
                Description = "Yellow, tasty"
            };
        }

		[Fact]
		public void OnPropertyChanged()
		{
			PropertyChangedEventArgs updated = null;
			_item.PropertyChanged += (sender, args) =>
			{
				updated = args;
			};

			_item.Name = "Hammer";

			Assert.NotNull(updated);
			Assert.Equal(nameof(_item.Name), updated.PropertyName);
		}

		[Fact]
		public void OnDidntChange()
		{
			_item.Name = "Hammer";

			PropertyChangedEventArgs updated = null;
			_item.PropertyChanged += (sender, args) =>
			{
				updated = args;
			};

			_item.Name = "Hammer";

			Assert.Null(updated);
		}

		[Fact]
		public void OnChangedEvent()
		{
			var triggered = false;
			_item.Changed = () =>
			{
				triggered = true;
			};

			_item.Name = "Cloth";

			Assert.True(triggered);
		}

		[Fact]
		public void ValidateEvent()
		{
			var contol = "Mammoth";
			var triggered = false;
			_item.Validate = (oldValue, newValue) =>
			{
				triggered = true;
				return oldValue != newValue;
			};

			_item.Name = contol;

			Assert.True(triggered);
			Assert.Equal(_item.Name, contol);

		}

		[Fact]
		public void NotValidateEvent()
		{
			var control = _item.Name;
			var triggered = false;
			_item.Validate = (oldValue, newValue) =>
			{
				triggered = true;
				return false;
			};

			_item.Name = "Merrow";

			Assert.True(triggered);
			Assert.Equal(_item.Name, control);

		}

		[Fact]
		public void ValidateEventException()
		{
			_item.Validate = (oldValue, newValue) =>
			{
				throw new ArgumentOutOfRangeException();
			};

			Assert.Throws<ArgumentOutOfRangeException>(() => _item.Name = "Horse");
		}
	}
}
