using System;

namespace DarkHelpers.Collections
{
    public interface IDarkObservableCollectionSynchronizer
    {
        void EnableSynchronization(IDarkObservableCollection collection);
        void HandleAction(Action action);
    }
}
