using DarkHelpers.Abstractions;
using DarkHelpers.Navigation;
using System.Threading.Tasks;
using System.Windows;

namespace DarkHelpers.WPF
{
    public class DarkWpfNavigationService : DarkNavigationServiceBase, IDarkNavigationService
    {
        private readonly DarkWpfNavigationHandler _darkNavigationHandler;

        public DarkWpfNavigationService()
        {
            _darkNavigationHandler = new DarkWpfNavigationHandler();
        }

        public void Register<TViewModel, TView>() where TViewModel : DarkViewModel where TView : DarkWpfViewBase<TViewModel>
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
            await _darkNavigationHandler.PushAsync(page as Window);
        }

        public async Task PopAsync()
        {
            await _darkNavigationHandler.PopAsync();
        }
    }
}
