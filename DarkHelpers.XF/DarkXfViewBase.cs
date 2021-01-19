using Xamarin.Forms;

namespace DarkHelpers.XF
{
    public class DarkXfViewBase<TViewModel> : ContentPage where TViewModel : DarkViewModel
    {
        private bool _initialized;

        private readonly TViewModel _viewModel;

        public DarkXfViewBase(TViewModel viewModel)
        {
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!_initialized)
            {
                await _viewModel.OnInitializeAsync();
                _initialized = true;
            }
            await _viewModel.OnRefreshAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await _viewModel.OnExitAsync();
        }
    }
}
