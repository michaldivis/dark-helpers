using DarkHelpers.XF;
using $rootnamespace$.ViewModels;
using Xamarin.Forms.Xaml;

namespace $rootnamespace$
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : DarkXfViewBase<$safeitemname$Model>
    {
        public HomeView($safeitemname$Model vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}