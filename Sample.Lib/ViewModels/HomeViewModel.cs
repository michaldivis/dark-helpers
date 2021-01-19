using DarkHelpers;
using DarkHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sample.Lib.ViewModels
{
    public class HomeViewModel : DarkViewModel
    {
        public ICommand ObservableCollectionCommand => new DarkAsyncCommand(OpenObservableCollectionAsync);
        public ICommand CommandsCommand => new DarkAsyncCommand(OpenCommandsAsync);

        private async Task OpenObservableCollectionAsync()
        {
            await FakeDiContainer.DarkNavigationService.PushAsync(new ObservableCollectionViewModel());
        }

        private async Task OpenCommandsAsync()
        {
            await FakeDiContainer.DarkNavigationService.PushAsync(new CommandsViewModel());
        }
    }
}
