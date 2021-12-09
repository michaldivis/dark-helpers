using System;

namespace DarkHelpers.Collections
{
    public static class DarkObservableCollectionSettings
    {
        private static IDarkObservableCollectionSynchronizer _synchronizer;

        public static void RegisterSynchronizer<TSynchronizer>() where TSynchronizer : IDarkObservableCollectionSynchronizer, new()
        {
            var instance = Activator.CreateInstance<TSynchronizer>();
            _synchronizer = instance;
        }

        internal static IDarkObservableCollectionSynchronizer GetSynchronizer()
        {
            if(_synchronizer is null)
            {
                _synchronizer = new NullDarkObservableCollectionSynchronizer();
            }

            return _synchronizer;
        }
    }
}
