using DarkHelpers.Collections;
using System;
using System.Windows.Data;

namespace DarkHelpers.WPF
{
    public class DarkWpfSynchronizer : IDarkObservableCollectionSynchronizer
    {
        public void EnableSynchronization(IDarkObservableCollection collection, object syncLock)
        {
            BindingOperations.EnableCollectionSynchronization(collection, syncLock);            
        }

        public void HandleAction(Action action, object syncLock)
        {
            lock (syncLock)
            {
                action?.Invoke();
            }
        }
    }
}
