using DarkHelpers.Tests.TestModels;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace DarkHelpers.Tests
{
    public class DarkObservableObjectTests
	{
		Item _item;

		[SetUp]
		public void Setup()
		{
            _item = new Item
            {
                Name = "Cheese",
                Description = "Yellow, tasty"
            };
        }

		[Test]
		public void OnPropertyChanged()
		{
			PropertyChangedEventArgs updated = null;
			_item.PropertyChanged += (sender, args) =>
			{
				updated = args;
			};

			_item.Name = "Hammer";

			Assert.IsNotNull(updated, "Property changed didn't raise");
			Assert.AreEqual(updated.PropertyName, nameof(_item.Name), "Correct Property name didn't get raised");
		}

		[Test]
		public void OnDidntChange()
		{
			_item.Name = "Hammer";

			PropertyChangedEventArgs updated = null;
			_item.PropertyChanged += (sender, args) =>
			{
				updated = args;
			};

			_item.Name = "Hammer";

			Assert.IsNull(updated, "Property changed was raised, but shouldn't have been");
		}

		[Test]
		public void OnChangedEvent()
		{
			var triggered = false;
			_item.Changed = () =>
			{
				triggered = true;
			};

			_item.Name = "Cloth";

			Assert.IsTrue(triggered, "OnChanged didn't raise");
		}

		[Test]
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

			Assert.IsTrue(triggered, "ValidateValue didn't raise");
			Assert.AreEqual(_item.Name, contol, "Value was not set correctly.");

		}

		[Test]
		public void NotValidateEvent()
		{
			var contol = _item.Name;
			var triggered = false;
			_item.Validate = (oldValue, newValue) =>
			{
				triggered = true;
				return false;
			};

			_item.Name = "Merrow";

			Assert.IsTrue(triggered, "ValidateValue didn't raise");
			Assert.AreEqual(_item.Name, contol, "Value should not have been set.");

		}

		[Test]
		public void ValidateEventException()
		{
			_item.Validate = (oldValue, newValue) =>
			{
				throw new ArgumentOutOfRangeException();
			};

			Assert.Throws(typeof(ArgumentOutOfRangeException), () => _item.Name = "Horse", "Should throw ArgumentOutOfRangeException");
		}
	}
}
