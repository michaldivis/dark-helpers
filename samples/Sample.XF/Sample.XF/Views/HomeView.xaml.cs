using DarkHelpers.XF;
using Sample.Lib.ViewModels;
using Xamarin.Forms.Xaml;

namespace Sample.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : DarkXfViewBase<HomeViewModel>
    {
        public HomeView(HomeViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}