# Why this?
[Avoid switch statements](https://youtu.be/7EmboKQH8lM?t=4570)

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
