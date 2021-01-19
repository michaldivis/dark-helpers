using System.Threading.Tasks;
using Xamarin.Forms;

namespace DarkHelpers.XF
{
    internal class DarkXfNavigationHandler
    {
        private INavigation GetNavigation()
        {
            return Application.Current.MainPage.Navigation;
        }

        public async Task PopAsync()
        {
            await GetNavigation().PopAsync();
        }

        public async Task PushAsync<TView>(TView view) where TView : ContentPage
        {
            await GetNavigation().PushAsync(view);
        }
    }
}
