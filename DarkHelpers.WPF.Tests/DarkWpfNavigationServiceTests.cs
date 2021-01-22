using DarkHelpers.WPF.Tests.HelperModels;
using Xunit;

namespace DarkHelpers.WPF.Tests
{
    public class DarkWpfNavigationServiceTests
    {
        private DarkWpfNavigationService _nav;

        public DarkWpfNavigationServiceTests()
        {
            _nav = new DarkWpfNavigationService();
        }

        [Fact]
        public void Register_ArgumentsCorrect_Passes()
        {
            _nav.Register<SomeViewModel, SomeView>();
        }
    }
}