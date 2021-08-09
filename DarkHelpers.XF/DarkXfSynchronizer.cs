using DarkHelpers.Collections;
using System;
using System.Collections;
using Xamarin.Forms;

namespace DarkHelpers.XF
{
    public class DarkXfSynchronizer : IDarkObservableCollectionSynchronizer
    {
        public void EnableSynchronization(IDarkObservableCollection collection)
        {
            BindingBase.EnableCollectionSynchronization(collection, null, ObservableCollectionCallback);
        }

        public void HandleAction(Action action)
        {
            Device.BeginInvokeOnMainThread(action);
        }

        private void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
        {
            lock (collection)
            {
                accessMethod?.Invoke();
            }
        }
    }
}
