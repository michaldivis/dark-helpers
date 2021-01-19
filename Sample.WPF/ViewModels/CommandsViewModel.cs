using DarkHelpers;
using DarkHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sample.WPF.ViewModels
{
    public class CommandsViewModel : DarkViewModel
    {
        public ICommand NormalCommand => new DarkCommand(WaitSynchronously);
        public ICommand AsynchronousCommand => new DarkAsyncCommand(WaitAsynchronously);

        private void WaitSynchronously()
        {
            IsBusy = true;
            Task.Delay(2000).Wait();
            IsBusy = false;
        }

        private async Task WaitAsynchronously()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}
