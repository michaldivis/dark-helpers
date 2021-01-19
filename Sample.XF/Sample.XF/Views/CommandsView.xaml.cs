using DarkHelpers.XF;
using Sample.Lib.ViewModels;
using Xamarin.Forms.Xaml;

namespace Sample.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandsView : DarkXfViewBase<CommandsViewModel>
    {
        public CommandsView(CommandsViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}