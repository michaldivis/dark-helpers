using System;
using System.ComponentModel;
using System.Windows;

namespace DarkHelpers.WPF
{
    public class DarkWpfViewBase<TViewModel> : Window where TViewModel : DarkViewModel
    {
        private readonly TViewModel _viewModel;

        public DarkWpfViewBase(TViewModel viewModel)
        {
            DataContext = _viewModel = viewModel;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await _viewModel.InitializeAsync();
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var cancelClosing = await _viewModel.OnBeforeExitAsync();

            e.Cancel = cancelClosing;

            if (!cancelClosing)
            {
                await _viewModel.OnExitAsync();
            }
        }
    }
}
