using DarkHelpers.XF.Tests.HelperModels;
using NUnit.Framework;

namespace DarkHelpers.XF.Tests
{
    public class DarkXfNavigationServiceTests
    {
        private DarkXfNavigationService _nav;

        [SetUp]
        public void CreateNavInstance()
        {
            _nav = new DarkXfNavigationService();
        }

        [Test]
        public void Register_ArgumentsCorrect_Passes()
        {
            _nav.Register<SomeViewModel, SomeView>();
        }
    }
}
