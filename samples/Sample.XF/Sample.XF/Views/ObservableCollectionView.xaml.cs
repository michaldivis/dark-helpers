using DarkHelpers.XF;
using Sample.Lib.ViewModels;
using Xamarin.Forms.Xaml;

namespace Sample.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObservableCollectionView : DarkXfViewBase<ObservableCollectionViewModel>
    {
        public ObservableCollectionView(ObservableCollectionViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}