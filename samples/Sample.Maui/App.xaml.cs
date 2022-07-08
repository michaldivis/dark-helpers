using DarkHelpers.Collections;
using DarkHelpers.Maui;
using Sample.Lib.ViewModels;
using Sample.Lib;
using DarkHelpers.Abstractions;
using Sample.Maui.Views;

namespace Sample.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        DarkObservableCollectionSettings.RegisterSynchronizer<DarkMauiSynchronizer>();

        InitializeFakeDiContainer();

        MainPage = new NavigationPage(new HomeView(new HomeViewModel()));
    }

    private void InitializeFakeDiContainer()
    {
        var nav = CreateNavigationService();
        FakeDiContainer.Initialize(nav);
    }

    private IDarkNavigationService CreateNavigationService()
    {
        var nav = new DarkMauiNavigationService();
        nav.Register<HomeViewModel, HomeView>();
        nav.Register<ObservableCollectionViewModel, ObservableCollectionView>();
        nav.Register<CommandsViewModel, CommandsView>();
        return nav;
    }
}
