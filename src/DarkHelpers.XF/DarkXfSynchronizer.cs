using DarkHelpers.Collections;
using System;
using System.Collections;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DarkHelpers.XF
{
    public class DarkXfSynchronizer : IDarkObservableCollectionSynchronizer
    {
        public void EnableSynchronization(IDarkObservableCollection collection, object syncLock)
        {
            BindingBase.EnableCollectionSynchronization(collection, syncLock, ObservableCollectionCallback);
        }

        public void HandleAction(Action action, object syncLock)
        {
            if (MainThread.IsMainThread)
            {
                action?.Invoke();
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(action);
            }
        }

        private void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
        {
            lock (context)
            {
                accessMethod?.Invoke();
            }
        }
    }
}
