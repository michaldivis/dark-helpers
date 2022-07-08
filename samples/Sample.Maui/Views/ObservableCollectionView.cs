using CommunityToolkit.Maui.Markup;
using DarkHelpers.Maui;
using Sample.Lib.Models;
using Sample.Lib.ViewModels;

namespace Sample.Maui.Views;

public class ObservableCollectionView : DarkMauiViewBase<ObservableCollectionViewModel>
{
    public ObservableCollectionView(ObservableCollectionViewModel vm) : base(vm)
    {
        Title = "Observable collection demo";

        Content = new ScrollView()
        {
            Content = new VerticalStackLayout
            {
                Spacing = 10,
                Padding = 5,
                Children = {
                    new Label()
                        .Text("Clear"),

                    new Button()
                        .Margin(5)
                        .Text("Sync")
                        .Bind(Button.CommandProperty, nameof(vm.ClearCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Button()
                        .Margin(5)
                        .Text("Async")
                        .Bind(Button.CommandProperty, nameof(vm.ClearAsyncCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Label()
                        .Text("Add item"),

                    new Button()
                        .Margin(5)
                        .Text("Sync")
                        .Bind(Button.CommandProperty, nameof(vm.AddItemCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Button()
                        .Margin(5)
                        .Text("Async")
                        .Bind(Button.CommandProperty, nameof(vm.AddItemAsyncCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Label()
                        .Text("Add items"),

                    new Button()
                        .Margin(5)
                        .Text("Sync")
                        .Bind(Button.CommandProperty, nameof(vm.AddItemsCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Button()
                        .Margin(5)
                        .Text("Async")
                        .Bind(Button.CommandProperty, nameof(vm.AddItemsAsyncCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Label()
                        .Text("Replace with item"),

                    new Button()
                        .Margin(5)
                        .Text("Sync")
                        .Bind(Button.CommandProperty, nameof(vm.ReplaceWithItemCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Button()
                        .Margin(5)
                        .Text("Async")
                        .Bind(Button.CommandProperty, nameof(vm.ReplaceWithItemAsyncCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Label()
                        .Text("Replace with items"),

                    new Button()
                        .Margin(5)
                        .Text("Sync")
                        .Bind(Button.CommandProperty, nameof(vm.ReplaceWithItemsCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new Button()
                        .Margin(5)
                        .Text("Async")
                        .Bind(Button.CommandProperty, nameof(vm.ReplaceWithItemsAsyncCommand))
                        .Bind(Button.IsEnabledProperty, nameof(vm.IsNotBusy)),

                    new VerticalStackLayout()
                        .Bind(BindableLayout.ItemsSourceProperty, nameof(vm.Items))
                        .ItemTemplate(new DataTemplate(() => new Label().Bind(Label.TextProperty, nameof(Item.Name))))
                }
            }
        };
    }
}
