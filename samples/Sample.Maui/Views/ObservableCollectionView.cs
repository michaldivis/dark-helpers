using DarkHelpers.Maui;
using Sample.Lib.ViewModels;

namespace Sample.Maui.Views;

public class ObservableCollectionView : DarkMauiViewBase<ObservableCollectionViewModel>
{
    public ObservableCollectionView(ObservableCollectionViewModel vm) : base(vm)
    {
        Title = "Observable collection demo";

        Content = new VerticalStackLayout
        {
            Spacing = 10,
            Padding = 10,
            Children = {
                new Label()
            }
        };
    }
}
