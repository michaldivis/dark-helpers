using CommunityToolkit.Maui.Markup;
using DarkHelpers.Maui;
using Sample.Lib.ViewModels;

namespace Sample.Maui.Views;

public class HomeView : DarkMauiViewBase<HomeViewModel>
{
	public HomeView(HomeViewModel vm) : base(vm)
	{
        var toUpperConverter = new FuncConverter<string>(
			convert: (text) => text?.ToUpper(),
			convertBack: (value) => value as string);

		Title = "Home";

        Content = new VerticalStackLayout
		{
			Spacing = 10,
			Padding = 10,
			Children = {
				new Label()
					.Margin(5)
					.Bind(Label.TextProperty, path: nameof(vm.SomeText), converter: toUpperConverter),

				new Button()
					.Margin(5)
					.Text("Observable collection demo")
					.Bind(Button.CommandProperty, nameof(vm.ObservableCollectionCommand))
					.Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                new Button()
                    .Margin(5)
                    .Text("Commands demo")
                    .Bind(Button.CommandProperty, nameof(vm.CommandsCommand))
                    .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy))
            }
		};
	}
}
