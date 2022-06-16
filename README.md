# DarkHelpers
A collection of MVVM helpers for Xamarin.Forms and WPF.

I wanted to create this library for my self, however, I'll be stoked if anyone else finds it useful. Feel free to request more features or changes.

*Code & inspiration:
I've used the `ObservableObject`, `ObservableRangeCollection` and `ICommand` implementations from [James Montemagno's MVVM-Helpers library](https://github.com/jamesmontemagno/mvvm-helpers), with some minor tweaks here and there. Thank you James!*

Status:

![Unit tests](https://github.com/michaldivis/DarkHelpers/workflows/Unit%20tests/badge.svg)

## Packages
[![license](https://img.shields.io/github/license/michaldivis/DarkHelpers?color=black&label=license&logo=Github&style=flat-square)](https://github.com/michaldivis/DarkHelpers/blob/master/README.md) 

### DarkHelpers
General package, includes all the interfaces and some implementations.

[![nuget](https://img.shields.io/nuget/v/Divis.DarkHelpers?label=version&color=black&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/Divis.DarkHelpers) [![nuget](https://img.shields.io/nuget/dt/Divis.DarkHelpers?color=black&label=downloads&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/Divis.DarkHelpers)

### DarkHelpers.XF
Platform specific implementations of base view and navigation service for Xamarin.Forms.

[![nuget](https://img.shields.io/nuget/v/Divis.DarkHelpers.XF?label=version&color=black&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/Divis.DarkHelpers.XF) [![nuget](https://img.shields.io/nuget/dt/Divis.DarkHelpers.XF?color=black&label=downloads&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/Divis.DarkHelpers.XF)

### DarkHelpers.WPF
Platform specific implementations of base view and navigation service for WPF

[![nuget](https://img.shields.io/nuget/v/Divis.DarkHelpers.WPF?label=version&color=black&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/Divis.DarkHelpers.WPF) [![nuget](https://img.shields.io/nuget/dt/Divis.DarkHelpers.WPF?color=black&label=downloads&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/Divis.DarkHelpers.WPF)

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

```csharp
public DarkObservableCollection<Item> Items { get; set; } = new DarkObservableCollection<Item>();

private void ReloadItems(){
    var items = _dataService.GetItems();
    Items.ReplaceRange(items);
}
```

#### Enable synchronization
Collection synchronization can be enabled using the static `DarkObservableCollectionSettings` class. Since collection synchronization is implemented differently for WPF and Xamarin.Forms, the initialization differs a bit.

WPF (App.xaml.cs)
```csharp
using DarkHelpers.Collections;

protected override void OnStartup(StartupEventArgs e)
{
    DarkObservableCollectionSettings.RegisterSynchronizer<DarkWpfSynchronizer>();

    //other code
}
```

Xamarin.Forms (App.xaml.cs)
```csharp
using DarkHelpers.Collections;

 public App()
{
    DarkObservableCollectionSettings.RegisterSynchronizer<DarkXfSynchronizer>();

    //other code
}
```

### IDarkNavigationService
Navigate freely, even from a class library where you might be storing your view models.

It works like this:
- register the ViewModel & View pairs to the instance of the platform specific nagivation service (`DarkXfNavigationService` for Xamarin.Forms and `DarkWpfNavigationService` for WPF). The view model has to implement the `DarkViewModel` class and the view has to implement the specific view base class (`DarkWpfViewBase` for WPF and `DarkXfViewBase` class for Xamarin.Forms). More on that in the "Custom base view" section
- store the platform specific implementation as an instance of `IDarkNavigationService` in a DI container (or wherever)
- only use the `IDarkNavigationService` instance to perform navigation 

#### ViewModel & View registration
Xamarin.Forms:
```csharp
using DarkHelpers.Abstractions;
using DarkHelpers.XF;

var nav = new DarkXfNavigationService();
nav.Register<HomeViewModel, HomeView>();
nav.Register<ObservableCollectionViewModel, ObservableCollectionView>();
nav.Register<CommandsViewModel, CommandsView>();

someContainer.RegisterSingleton<IDarkNavigationService>(nav);
```

WPF:
```csharp
using DarkHelpers.Abstractions;
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

### Custom base view

Custom base view that allows the navigation by view model.

*NOTE: you can now use my Visual Studio extension that contains view templates for both WPF and Xamarin.Forms for easier view creation. The extension can be found [HERE](https://marketplace.visualstudio.com/items?itemName=michaldivis.DarkHelpersTemplates)*

#### Creating a WPF view

Create a new `Window` and change the code to look like this.

*I'm using the SomeApp as my example project namespace*

SomeView.xaml
```xaml
<darkViews:DarkWpfViewBase
    x:Class="SomeApp.Views.SomeView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:darkViews="clr-namespace:DarkHelpers.WPF;assembly=DarkHelpers.WPF"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:viewModels="clr-namespace:SomeApp.ViewModels;assembly=SomeApp"
    Title="SomeView"
    x:TypeArguments="viewModels:SomeViewModel"
    mc:Ignorable="d">
    <Grid />
</darkViews:DarkWpfViewBase>
```

SomeView.xaml.cs
```csharp
using DarkHelpers.WPF;
using SomeApp.ViewModels;

namespace SomeApp.Views
{
    public partial class SomeView : DarkWpfViewBase<SomeViewModel>
    {
        public HomeView(SomeViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
```

#### Creating a Xamarin.Forms view

Create a new `ContentPage` and change the code to look like this.

*I'm using the SomeApp as my example project namespace*

SomeView.xaml
```xaml
<?xml version="1.0" encoding="utf-8" ?>
<darkViews:DarkXfViewBase
    x:Class="SomeApp.Views.SomeView"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:darkViews="clr-namespace:DarkHelpers.XF;assembly=DarkHelpers.XF"
    xmlns:viewModels="clr-namespace:SomeApp.ViewModels;assembly=SomeApp"
    Title="Home"
    x:TypeArguments="viewModels:SomeViewModel">
    <ContentPage.Content>
        <Grid />
    </ContentPage.Content>
</darkViews:DarkXfViewBase>
```

SomeView.xaml.cs
```csharp
using DarkHelpers.XF;
using SomeApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace SomeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SomeView : DarkXfViewBase<SomeViewModel>
    {
        public HomeView(SomeViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}
```

### Extensions
#### SafeFireAndForget (for `Task`)
Safely fire and forget a `Task` while being able to handle exceptions. Sometimes you don't care about waiting for a `Task` to complete or you can't await it (in an event). That's what this extension is for.
```csharp
using DarkHelpers;

protected override void OnAppearing()
{
    //use an Action to handle exceptions
    Action<Exception> errorHandler = (ex) => Console.WriteLine(ex);
    DoSomethingAsync().SafeFireAndForget(errorHandler);

    //or use the ITaskErrorHandler to handle exceptions
    ITaskErrorHandler taskErrorHandler = null; //this might get injected by DI
    DoSomethingAsync().SafeFireAndForget(taskErrorHandler);
}

private async Task DoSomethingAsync()
{
    //simulate work
    await Task.Delay(500);
}
```

#### TryExecute (for `ICommand`)
This is useful for executing `ICommand`s manually. It checks the `CanExecute` and calls the `Execute` method if `CanExecute` returns `true`.

```csharp
using DarkHelpers;

//this might be called from a code behind of a page if there's no option to bind the command in XAML
Book book = null;
viewModel.LoadItemCommand.TryExecute(book);
```

### Converter base classes
Simplify your converters with the `DarkConverterBase` (`IValueConverter`) and `DarkMultiConverterBase` (`IMultiValueConverter`) base classes.

Simply inherit from either of these base classes and only override the methods you need (`Convert` or `ConvertBack` or both). The rest will throw a verbose `NotSupportedException`.

#### 
Before:
```csharp
public class ToUpperTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is string text)
        {
            return text.ToUpper();
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException($"The {nameof(ConvertBack)} method is not supported in {nameof(ToUpperTextConverter)}.");
    }
}
```

After:
```csharp
public class ToUpperCaseTextConverter : DarkConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string text)
        {
            return text.ToUpper();
        }

        return value;
    }
}
```
