using System;

namespace DarkHelpers.Collections
{
    internal class NullDarkObservableCollectionSynchronizer : IDarkObservableCollectionSynchronizer
    {
        public void EnableSynchronization(IDarkObservableCollection collection, object syncLock)
        {
            //do nothing
        }

        public void HandleAction(Action action, object syncLock)
        {
            action?.Invoke();
        }
    }
}
