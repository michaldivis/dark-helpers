using System;
using System.Collections.Generic;

namespace DarkHelpers.Navigation
{
    public class DarkNavigationServiceBase
    {
        protected readonly Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();

        protected object GetPageByViewModel<TViewModel>(TViewModel viewModel) where TViewModel : DarkViewModel
        {
            var viewModelType = viewModel.GetType();
            var viewModelTypeFound = _registeredTypes.TryGetValue(viewModelType, out var viewType);

            if (!viewModelTypeFound)
            {
                throw new Exception($"No view has been registered for the VM class {typeof(TViewModel)}. Make sure to register it via the {nameof(GetPageByViewModel)} method first.");
            }

            var viewCtor = viewType.GetConstructor(new Type[] { viewModelType });

            if (viewCtor != null)
            {
                var view = viewCtor.Invoke(new object[] { viewModel });
                return view;
            }

            throw new Exception($"View of type {viewType} doesn't have a constructor which takes {typeof(TViewModel)}. The view cannot be constructed.");
        }

        protected DarkViewModel TryToCreateViewModel<TViewModel>() where TViewModel : DarkViewModel, new()
        {
            var viewModelType = typeof(TViewModel);
            var viewModelCtor = viewModelType.GetConstructor(Type.EmptyTypes);

            if (viewModelCtor != null)
            {
                var viewModel = viewModelCtor.Invoke(null);
                return (TViewModel)viewModel;
            }

            throw new Exception($"The view model of type {typeof(TViewModel)} doesn't have a default constructor and thus cannot be constructed without parameters.");
        }
    }
}
