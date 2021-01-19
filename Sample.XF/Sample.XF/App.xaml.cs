using DarkHelpers.Interfaces;
using DarkHelpers.XF;
using Sample.Lib;
using Sample.Lib.ViewModels;
using Sample.XF.Views;
using Xamarin.Forms;

namespace Sample.XF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeFakeDiContainer();

            MainPage = new NavigationPage(new HomeView(new HomeViewModel()));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void InitializeFakeDiContainer()
        {
            var nav = CreateNavigationService();
            FakeDiContainer.Initialize(nav);
        }

        private IDarkNavigationService CreateNavigationService()
        {
            var nav = new DarkXfNavigationService();
            nav.Register<HomeViewModel, HomeView>();
            nav.Register<ObservableCollectionViewModel, ObservableCollectionView>();
            nav.Register<CommandsViewModel, CommandsView>();
            return nav;
        }
    }
}
