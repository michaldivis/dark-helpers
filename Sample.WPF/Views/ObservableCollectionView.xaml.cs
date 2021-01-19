using DarkHelpers.WPF;
using Sample.WPF.ViewModels;

namespace Sample.WPF.Views
{
    public partial class ObservableCollectionView : DarkWpfViewBase<ObservableCollectionViewModel>
    {
        public ObservableCollectionView(ObservableCollectionViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
