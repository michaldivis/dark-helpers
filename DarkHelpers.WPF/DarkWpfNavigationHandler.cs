using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DarkHelpers.WPF
{
    internal class DarkWpfNavigationHandler
    {
        private Window GetCurrentActiveWindow()
        {
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }

        public async Task PopAsync()
        {
            GetCurrentActiveWindow().Close();
        }

        public async Task PushAsync<TView>(TView view) where TView : Window
        {
            view.Show();
        }
    }
}
