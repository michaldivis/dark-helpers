using System.Linq;
using System.Windows;

namespace DarkHelpers.WPF
{
    internal class DarkWpfNavigationHandler
    {
        private static Window? GetCurrentActiveWindow()
        {
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }

        public static void Pop()
        {
            GetCurrentActiveWindow()?.Close();
        }

        public static void Push<TView>(TView view) where TView : Window
        {
            view.Show();
        }
    }
}
