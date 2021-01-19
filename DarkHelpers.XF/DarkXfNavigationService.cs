using DarkHelpers.Interfaces;
using DarkHelpers.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DarkHelpers.XF
{
    public class DarkXfNavigationService : DarkNavigationServiceBase, IDarkNavigationService
    {
        private readonly DarkXfNavigationHandler _darkNavigationHandler;

        public DarkXfNavigationService()
        {
            _darkNavigationHandler = new DarkXfNavigationHandler();
        }

        public void Register<TViewModel, TView>() where TViewModel : DarkViewModel where TView : DarkXfViewBase<TViewModel>
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
            var page = GetPageByViewModel(viewModel);
            await _darkNavigationHandler.PushAsync(page as ContentPage);
        }

        public async Task PopAsync()
        {
            await _darkNavigationHandler.PopAsync();
        }
    }
}
