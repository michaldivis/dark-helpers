using DarkHelpers.Collections;
using System.Collections;

namespace DarkHelpers.Maui;

public class DarkMauiSynchronizer : IDarkObservableCollectionSynchronizer
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
