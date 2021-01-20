using DarkHelpers.WPF.Tests.HelperModels;
using NUnit.Framework;

namespace DarkHelpers.WPF.Tests
{
    public class DarkWpfNavigationServiceTests
    {
        private DarkWpfNavigationService _nav;

        [SetUp]
        public void CreateNavInstance()
        {
            _nav = new DarkWpfNavigationService();
        }

        [Test]
        public void Register_ArgumentsCorrect_Passes()
        {
            _nav.Register<SomeViewModel, SomeView>();
        }
    }
}