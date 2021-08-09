namespace DarkHelpers.Collections
{
    public static class DarkObservableCollectionSettings
    {
        private static IDarkObservableCollectionSynchronizer _synchronizer;

        public static void RegisterSynchronizer(IDarkObservableCollectionSynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
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
