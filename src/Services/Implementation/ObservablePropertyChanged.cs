using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace ObservableProperty.Services.Implementation;

public class ObservablePropertyChanged : IObservablePropertyChanged
{
    private object actionCollection = null!;
    private MethodInfo? methodInfo;

    public ActionCollection<TModel> Observe<TModel>(TModel target) where TModel : INotifyPropertyChanged
    {
        RegisterPropertyChanged(target);
        return BuildActionCollection<TModel>();
    }

    private void RegisterPropertyChanged<TModel>(TModel target) where TModel : INotifyPropertyChanged
    {
        target.PropertyChanged -= Model_PropertyChanged;
        target.PropertyChanged += Model_PropertyChanged;
    }

    private ActionCollection<TModel> BuildActionCollection<TModel>() where TModel : notnull
    {
        var type = typeof(ActionCollection<TModel>);
        methodInfo = type.GetMethod(nameof(ActionCollection<TModel>.Get));
        actionCollection = new ActionCollection<TModel>();
        return (ActionCollection<TModel>)actionCollection;
    }

    private async void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var actionList = GetActionList();
        var propertyName = e.PropertyName!;
        if (!actionList.ContainsKey(propertyName))
            return;

        var type = sender!.GetType();
        var property = type.GetProperty(propertyName)!;
        var value = property.GetValue(sender, null);
        var action = actionList[propertyName];
        await action(value!);
    }

    private IReadOnlyDictionary<string, Func<object, Task>> GetActionList()
    {
        if (methodInfo == null)
            throw new InvalidOperationException($"{nameof(methodInfo)} is null.");
        var returnValue = methodInfo!.Invoke(actionCollection, null)!;
        return (IReadOnlyDictionary<string, Func<object, Task>>)returnValue;
    }
}
