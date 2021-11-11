using System.Threading.Tasks;

namespace DarkHelpers.Abstractions
{
    public interface IDarkNavigationService
    {
        Task PopAsync();
        Task PushAsync<TViewModel>() where TViewModel : DarkViewModel, new();
        Task PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : DarkViewModel;
    }
}