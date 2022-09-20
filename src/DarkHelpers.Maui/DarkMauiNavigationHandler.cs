namespace DarkHelpers.Maui;

internal class DarkMauiNavigationHandler
{
    private static INavigation? GetNavigation()
    {
        return Application.Current?.MainPage?.Navigation;
    }

    public static async Task PopAsync()
    {
        var nav = GetNavigation();

        if(nav is null)
        {
            return;
        }

        _ = await nav.PopAsync();
    }

    public static async Task PushAsync<TView>(TView view) where TView : ContentPage
    {
        var nav = GetNavigation();

        if (nav is null)
        {
            return;
        }

        await nav.PushAsync(view);
    }
}
