using System;

namespace DarkHelpers.Collections
{
    public interface IDarkObservableCollectionSynchronizer
    {
        void EnableSynchronization(IDarkObservableCollection collection, object syncLock);
        void HandleAction(Action action, object syncLock);
    }
}
