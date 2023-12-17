using CrossUtility.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ObservableProperty.Services;

public class ActionCollection<TModel> where TModel : notnull
{
    private readonly Dictionary<string, Func<object?, Task>> actionList = [];

    public ActionCollection<TModel> When<TPropertyValue>([NotNull] string propertyName, Action<TPropertyValue?> action)
    {
        actionList[propertyName] = (param) =>
        {
            action(param == null ? default : (TPropertyValue)param);
            return Task.CompletedTask;
        };
        return this;
    }

    public ActionCollection<TModel> When<TPropertyValue>([NotNull] string propertyName, Func<TPropertyValue?, Task> action)
    {
        actionList[propertyName] = (param) =>
        {
            return action(param == null ? default : (TPropertyValue)param);
        };
        return this;
    }

    public ActionCollection<TModel> When<TPropertyValue>(Expression<Func<TModel, TPropertyValue>> expression, Action<TPropertyValue?> action)
    {
        var propertyName = expression.GetMemberName();
        return When(propertyName, action);
    }

    public ActionCollection<TModel> When<TPropertyValue>(Expression<Func<TModel, TPropertyValue>> expression, Func<TPropertyValue?, Task> action)
    {
        var propertyName = expression.GetMemberName();
        return When(propertyName, action);
    }

    public IReadOnlyDictionary<string, Func<object?, Task>> Get() => actionList;
}
