using DarkHelpers.Abstractions;
using DarkHelpers.Navigation;

namespace DarkHelpers.Maui;

public class DarkMauiNavigationService : DarkNavigationServiceBase, IDarkNavigationService
{
    private readonly DarkMauiNavigationHandler _darkNavigationHandler;

    public DarkMauiNavigationService()
    {
        _darkNavigationHandler = new DarkMauiNavigationHandler();
    }

    public void Register<TViewModel, TView>() where TViewModel : DarkViewModel where TView : DarkMauiViewBase<TViewModel>
    {
        _registeredTypes.Add(typeof(TViewModel), typeof(TView));
    }

    public async Task PushAsync<TViewModel>() where TViewModel : DarkViewModel, new()
    {
        var viewModel = TryToCreateViewModel<TViewModel>();
        await PushAsync(viewModel);
    }

    public async Task PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : DarkViewModel
    {
        await viewModel.OnInitializeAsync();
        var page = GetViewByViewModel(viewModel);
        await _darkNavigationHandler.PushAsync(page as ContentPage);
    }

    public async Task PopAsync()
    {
        await _darkNavigationHandler.PopAsync();
    }
}
