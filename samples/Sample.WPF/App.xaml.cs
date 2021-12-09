using DarkHelpers.Collections;
using DarkHelpers.Abstractions;
using DarkHelpers.WPF;
using Sample.Lib;
using Sample.Lib.ViewModels;
using Sample.WPF.Views;
using System.Windows;

namespace Sample.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitializeFakeDiContainer();
            SetDefaultWindowStyle();

            DarkObservableCollectionSettings.RegisterSynchronizer<DarkWpfSynchronizer>();

            MainWindow = new HomeView(new HomeViewModel());
            MainWindow.Show();
        }

        private void InitializeFakeDiContainer()
        {
            var nav = CreateNavigationService();
            FakeDiContainer.Initialize(nav);
        }

        private IDarkNavigationService CreateNavigationService()
        {
            var nav = new DarkWpfNavigationService();
            nav.Register<HomeViewModel, HomeView>();
            nav.Register<ObservableCollectionViewModel, ObservableCollectionView>();
            nav.Register<CommandsViewModel, CommandsView>();
            return nav;
        }

        private void SetDefaultWindowStyle()
        {
            DarkWpfViewBase.WindowStyler = window => {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            };
        }
    }
}
