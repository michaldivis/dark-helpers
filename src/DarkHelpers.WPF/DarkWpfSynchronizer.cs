using DarkHelpers.Collections;
using System;
using System.Windows.Data;

namespace DarkHelpers.WPF
{
    public class DarkWpfSynchronizer : IDarkObservableCollectionSynchronizer
    {
        public void EnableSynchronization(IDarkObservableCollection collection)
        {
            BindingOperations.EnableCollectionSynchronization(collection, new object());
        }

        public void HandleAction(Action action)
        {
            action?.Invoke();
        }
    }
}
