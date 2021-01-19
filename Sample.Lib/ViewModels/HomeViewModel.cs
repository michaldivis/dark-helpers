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
            IsBusy = true;
            await FakeDiContainer.DarkNavigationService.PushAsync(new ObservableCollectionViewModel());
            IsBusy = false;
        }

        private async Task OpenCommandsAsync()
        {
            IsBusy = true;
            await FakeDiContainer.DarkNavigationService.PushAsync(new CommandsViewModel());
            IsBusy = false;
        }
    }
}
