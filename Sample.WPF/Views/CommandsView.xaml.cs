using DarkHelpers.WPF;
using Sample.WPF.ViewModels;

namespace Sample.WPF.Views
{
    public partial class CommandsView : DarkWpfViewBase<CommandsViewModel>
    {
        public CommandsView(CommandsViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
