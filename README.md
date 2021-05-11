# Install

| Package | NuGet Stable | Downloads |
| ------- | ------------ | --------- |
| [ObservableProperty](https://www.nuget.org/packages/ObservableProperty/) | [![ObservableProperty](https://img.shields.io/nuget/v/ObservableProperty.svg)](https://www.nuget.org/packages/ObservableProperty/) | [![ObservableProperty](https://img.shields.io/nuget/dt/ObservableProperty.svg)](https://www.nuget.org/packages/ObservableProperty/) |

# Why this?
[Avoid switch statements](https://youtu.be/7EmboKQH8lM?t=4578)

## Don't
```c#
public void override OnPropertyChanged([CallerMemberName]string? propertyName = null)
{
    switch (propertyName)
    {
        case nameof(FirstName):
            HandleFirstNameChanged(); // Generic name
            break;
        case nameof(LastName):
            HandleLastNameChanged(); // Generic name
            break;
        case nameof(EmailAddress):
            HandleEmailAddressChanged(); // Generic name
            break;
        case nameof(PhysicalAddress):
            HandlePhysicalAddressChanged(); // Generic name
            break;
        // ...
    }
}
```

## Do's
```c#
private void Initialize()
{
    observablePropertyChanged.Observe(this)
        .When(it => it.FirstName, DoThis)  // Should be more descriptive
        .When(it => it.LastName, DoThat) // but you get the idea.
        .When(it => it.EmailAddress, DoSomething) // the thing is,
        .When(it => it.PhysicalAddress, UpdateSomething); // I am too lazy atm.
}

```
