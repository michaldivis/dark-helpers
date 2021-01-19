using DarkHelpers;
using DarkHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sample.WPF.ViewModels
{
    public class HomeViewModel : DarkViewModel
    {
        public ICommand ObservableCollectionCommand => new DarkAsyncCommand(OpenObservableCollectionAsync);

        private async Task OpenObservableCollectionAsync()
        {
            await FakeDiContainer.DarkNavigationService.PushAsync(new ObservableCollectionViewModel());
        }
    }
}
