using CrossUtility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ObservableProperty.Services
{
    public class ActionCollection<TModel> where TModel : notnull
    {
        private readonly Dictionary<string, Func<object, Task>> actionList = new();

        public ActionCollection<TModel> When<TProperty>(Expression<Func<TModel, TProperty>> expression, Action<TProperty> action)
        {
            var propertyName = expression.GetMemberName();
            actionList[propertyName] = (param) =>
            {
                action((TProperty)param);
                return Task.CompletedTask;
            };
            return this;
        }

        public ActionCollection<TModel> When<TProperty>(Expression<Func<TModel, TProperty>> expression, Func<TProperty, Task> action)
        {
            var propertyName = expression.GetMemberName();
            actionList[propertyName] = (param) =>
            {
                return action((TProperty)param);
            };
            return this;
        }

        public IReadOnlyDictionary<string, Func<object, Task>> Get() => actionList;
    }
}
