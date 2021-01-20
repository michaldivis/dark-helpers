# DarkHelpers
A collection of MVVM helpers for Xamarin.Forms and WPF.

I wanted to create this library for my self, however, I'll be stoked if anyone else finds it useful. Feel free to request more features or changes.

*Code & inspiration:
I've used the `ObservableObject`, `ObservableRangeCollection` and `ICommand` implementations from [James Montemagno's MVVM-Helpers library](https://github.com/jamesmontemagno/mvvm-helpers), with some minor tweaks here and there. Thank you James!*

## Nuget
[![Nuget](https://img.shields.io/nuget/v/divis.darkhelpers?label=DarkHelpers)](https://www.nuget.org/packages/Divis.DarkHelpers/)
[![Nuget](https://img.shields.io/nuget/v/divis.darkhelpers.xf?label=DarkHelpers.XF)](https://www.nuget.org/packages/Divis.DarkHelpers.XF/)
[![Nuget](https://img.shields.io/nuget/v/divis.darkhelpers.wpf?label=DarkHelpers.WPF)](https://www.nuget.org/packages/Divis.DarkHelpers.WPF/)

## Packages
- `DarkHelpers` - general package, includes all the interfaces and some implementations.
- `DarkHelpers.XF` - platform specific implementations of base view and navigation service for Xamarin.Forms
- `DarkHelpers.WPF` - platform specific implementations of base view and navigation service for WPF

## Features

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
A base view model class that implements `INotifyPropertyChanged` and has all the properties and events you would usually need.

Properties:
- `IsBusy`
- `IsNotBusy`
- `CanLoadMore`
- `IsLoadingMore`

Events (virtual methods):
- `OnInitializeAsync` - One time initialzation method, to be called when the view model is first used
- `OnRefreshAsync` - To be called whenever the view model is brought to user's attention, i.e. anytime the corresponding view is shown
- `OnBeforeExitAsync` - To be called before a view is exited, determines whether the exit should be continued or cancelled
- `OnExitAsync` - To be called when a view is exiting, useful for any cleanup work

Event support:
|  | Xamarin.Forms | WPF |
| ------------- | ------------- | ------------- |
| `OnInitializeAsync` | Yes | Yes |
| `OnRefreshAsync` | Yes | No |
| `OnBeforeExitAsync` | No | Yes |
| `OnExitAsync` | Yes | Yes |

### DarkObservableCollection
A ObservableCollection that adds important methods such as: AddRange, RemoveRange, Replace, and ReplaceRange.

### IDarkNavigationService
Navigate freely, even from a class library where you might be storing your view models.

It works like this:
- register the ViewModel & View pairs to the instance of the platform specific nagivation service (`DarkXfNavigationService` for Xamarin.Forms and `DarkWpfNavigationService` for WPF)
- store the platform specific implementation as an instance of `IDarkNavigationService` in a DI container (or wherever)
- only use the `IDarkNavigationService` instance to perform navigation 

#### ViewModel & View registration
Xamarin.Forms:
```csharp
using DarkHelpers.Interfaces;
using DarkHelpers.XF;

var nav = new DarkXfNavigationService();
nav.Register<HomeViewModel, HomeView>();
nav.Register<ObservableCollectionViewModel, ObservableCollectionView>();
nav.Register<CommandsViewModel, CommandsView>();

someContainer.RegisterSingleton<IDarkNavigationService>(nav);
```

WPF:
```csharp
using DarkHelpers.Interfaces;
using DarkHelpers.WPF;

var nav = new DarkWpfNavigationService();
nav.Register<HomeViewModel, HomeView>();
nav.Register<ObservableCollectionViewModel, ObservableCollectionView>();
nav.Register<CommandsViewModel, CommandsView>();

someContainer.RegisterSingleton<IDarkNavigationService>(nav);
```

#### Usage
```csharp
using DarkHelpers;

var nav = someContainer.Get<IDarkNavigationService>();

await nav.PushAsync(new HomeViewModel());

await nav.PopAsync();
```

### DarkAsyncCommand, DarkCommand, and DarkEventManager

Synchronous and asynchronous ICommand implementations, plus a DarkEventManager to help your events be garbage collection safe
