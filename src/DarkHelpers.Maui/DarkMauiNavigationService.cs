using DarkHelpers.Abstractions;
using DarkHelpers.Navigation;

namespace DarkHelpers.Maui;

public class DarkMauiNavigationService : DarkNavigationServiceBase, IDarkNavigationService
{
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

        if(page is ContentPage contentPage)
        {
            await DarkMauiNavigationHandler.PushAsync(contentPage);
        }
    }

    public async Task PopAsync()
    {
        await DarkMauiNavigationHandler.PopAsync();
    }
}
