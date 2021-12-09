using System;

namespace DarkHelpers.Tests.TestModels
{
    public class Item : DarkObservableObject
    {
		public Action Changed { get; set; }

		public Func<string, string, bool> Validate { get; set; }

		string _name;
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value, onChanged: Changed, validateValue: Validate);
		}

		string _description;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value, onChanged: Changed, validateValue: Validate);
		}

        public string SortName => Name[0].ToString().ToUpperInvariant();
    }
}
