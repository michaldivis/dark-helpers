using System.Threading.Tasks;

namespace DarkHelpers.Interfaces
{
    public interface IDarkNavigationService
    {
        Task PopAsync();
        Task PushAsync<TViewModel>() where TViewModel : DarkViewModel, new();
        Task PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : DarkViewModel;
    }
}