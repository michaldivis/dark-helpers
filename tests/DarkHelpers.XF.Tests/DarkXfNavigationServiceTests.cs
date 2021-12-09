using DarkHelpers.XF.Tests.HelperModels;
using Xunit;

namespace DarkHelpers.XF.Tests
{
    public class DarkXfNavigationServiceTests
    {
        private DarkXfNavigationService _nav;

        public DarkXfNavigationServiceTests()
        {
            _nav = new DarkXfNavigationService();
        }

        [Fact]
        public void Register_ArgumentsCorrect_Passes()
        {
            _nav.Register<SomeViewModel, SomeView>();
        }
    }
}
