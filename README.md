## Why this?
[Switch case is bad](https://youtu.be/7EmboKQH8lM?t=4570)

```c#

// Convert this
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

// To this
private void Initialize()
{
    observablePropertyChanged.Observe(this)
        .When(it => it.FirstName, DoThis)  // Should be more descriptive
        .When(it => it.LastName, DoThat) // but you get the idea.
        .When(it => it.EmailAddress, DoSomething) // the thing is,
        .When(it => it.PhysicalAddress, UpdateSomething); // I am too lazy atm.
}

```
