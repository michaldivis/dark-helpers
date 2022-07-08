using CommunityToolkit.Maui.Markup;
using DarkHelpers.Maui;
using Sample.Lib.ViewModels;

namespace Sample.Maui.Views;

public class CommandsView : DarkMauiViewBase<CommandsViewModel>
{
    public CommandsView(CommandsViewModel vm) : base(vm)
    {
        Title = "Commands demo";

        Content = new VerticalStackLayout
        {
            Spacing = 10,
            Padding = 5,
            Children = {
                new Button()
                    .Margin(5)
                    .Text("Synchronous command")
                    .Bind(Button.CommandProperty, nameof(vm.NormalCommand))
                    .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                new Button()
                    .Margin(5)
                    .Text("Asynchronous command")
                    .Bind(Button.CommandProperty, nameof(vm.AsynchronousCommand))
                    .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy))
            }
        };
    }
}