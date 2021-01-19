using DarkHelpers.WPF;
using Sample.Lib.ViewModels;

namespace Sample.WPF.Views
{
    public partial class HomeView : DarkWpfViewBase<HomeViewModel>
    {
        public HomeView(HomeViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
