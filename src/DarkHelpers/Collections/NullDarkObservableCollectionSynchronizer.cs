using System;

namespace DarkHelpers.Collections
{
    internal class NullDarkObservableCollectionSynchronizer : IDarkObservableCollectionSynchronizer
    {
        public void EnableSynchronization(IDarkObservableCollection collection)
        {
            //do nothing
        }

        public void HandleAction(Action action)
        {
            action?.Invoke();
        }
    }
}
