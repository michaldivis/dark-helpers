namespace DarkHelpers.Maui;

internal class DarkMauiNavigationHandler
{
    private INavigation GetNavigation()
    {
        return Application.Current.MainPage.Navigation;
    }

    public async Task PopAsync()
    {
        _ = await GetNavigation().PopAsync();
    }

    public async Task PushAsync<TView>(TView view) where TView : ContentPage
    {
        await GetNavigation().PushAsync(view);
    }
}
