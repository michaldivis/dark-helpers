# DarkHelpers
A collection of MVVM helpers for Xamarin.Forms

## Nuget
[![Nuget](https://img.shields.io/nuget/v/divis.darkhelpers?label=DarkHelpers)](https://www.nuget.org/packages/Divis.DarkHelpers/)
[![Nuget](https://img.shields.io/nuget/v/divis.darkhelpers.xf?label=DarkHelpers.XF)](https://www.nuget.org/packages/Divis.DarkHelpers.XF/)
[![Nuget](https://img.shields.io/nuget/v/divis.darkhelpers.wpf?label=DarkHelpers.WPF)](https://www.nuget.org/packages/Divis.DarkHelpers.WPF/)

## Features

I wanted to create this library for my self, however, I'll be stoked if anyone else finds it useful. Feel free to request more features or changes.

### DarkObservableObject
Simple implementation of INotifyPropertyChanged that any class can inherit from.

```csharp
public class MyObject : DarkObservableObject
{
    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value, onChanged: DoSomething);
    }
    
    private void DoSomething()
    {
        //react to changes
    }
}
```

### DarkViewModel
A base view model class that has all the properties you would usually need:
- `bool` IsBusy
- `bool` IsNotBusy
- `bool` CanLoadMore
- `bool` IsLoadingMore

#### View model event support
|  | Xamarin.Forms | WPF |
| ------------- | ------------- | ------------- |
| OnInitializeAsync | Yes | Yes |
| OnRefreshAsync | Yes | No |
| OnBeforeExitAsync | No | Yes |
| OnExitAsync | Yes | Yes |



## Navigation service
