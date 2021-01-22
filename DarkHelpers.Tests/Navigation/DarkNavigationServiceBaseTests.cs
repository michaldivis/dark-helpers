using DarkHelpers.Navigation;
using System;
using System.Collections.Generic;
using Xunit;

namespace DarkHelpers.Tests.Navigation
{
    public class DarkNavigationServiceBaseTests
    {
        private TestableNav _nav;

        public DarkNavigationServiceBaseTests()
        {
            _nav = new TestableNav();
        }

        [Fact]
        public void GetViewByViewModel_ParameterNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _nav.GetViewByViewModel<SomeViewModel>(null));
        }

        [Fact]
        public void GetViewByViewModel_NotRegistered_ThrowsException()
        {
            Assert.Throws<Exception>(() => _nav.GetViewByViewModel(new SomeViewModel()));
        }

        [Fact]
        public void GetViewByViewModel_ViewDoesntHaveCorrectCtor_ThrowsException()
        {
            _nav._registeredTypes.Add(typeof(SomeViewModel), typeof(WrongView));
            Assert.Throws<Exception>(() => _nav.GetViewByViewModel(new SomeViewModel()));
        }

        [Fact]
        public void GetViewByViewModel_RegisteredCorrectly_Passes()
        {
            _nav._registeredTypes.Add(typeof(SomeViewModel), typeof(SomeView));
            var view = _nav.GetViewByViewModel(new SomeViewModel());
        }

        [Fact]
        public void TryToCreateViewModel_HasDefaultConstructor_Passes()
        {
            var vm = _nav.TryToCreateViewModel<SomeViewModel>();
        }
    }

    #region Helper classes

    class TestableNav : DarkNavigationServiceBase
    {
        public new Dictionary<Type, Type> _registeredTypes => base._registeredTypes;

        public new object GetViewByViewModel<TViewModel>(TViewModel viewModel) where TViewModel : DarkViewModel
        {
            return base.GetViewByViewModel(viewModel);
        }

        public new DarkViewModel TryToCreateViewModel<TViewModel>() where TViewModel : DarkViewModel, new()
        {
            return base.TryToCreateViewModel<TViewModel>();
        }
    }

    class SomeViewModel : DarkViewModel
    {

    }

    class SomeView
    {
        public SomeView(SomeViewModel vm)
        {

        }
    }

    class WrongView
    {

    }

    #endregion
}
