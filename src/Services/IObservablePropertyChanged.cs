using System.ComponentModel;

namespace ObservableProperty.Services;

public interface IObservablePropertyChanged
{
    ActionCollection<TModel> Observe<TModel>(TModel target) where TModel : INotifyPropertyChanged;
}
