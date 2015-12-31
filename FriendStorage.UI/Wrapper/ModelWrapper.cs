using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FriendStorage.Model;
using FriendStorage.UI.ViewModel;

namespace FriendStorage.UI.Wrapper
{
    public class ModelWrapper<T> : Observable
    {
        private readonly Dictionary<string, object> _orignalValues;


        public ModelWrapper(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            Model = model;
            _orignalValues = new Dictionary<string, object>();
        }

        public T Model { get; private set; }

        protected void SetValue<TValue>(TValue newValue, [CallerMemberName]string propertyName = null)
        {
            if (propertyName != null)
            {
                var propertyInfo = Model.GetType().GetProperty(propertyName);
                var currentValue = propertyInfo.GetValue(Model);
                if (!Equals(currentValue, newValue))
                {
                    UpdateOrignalValue(currentValue, propertyName);
                    propertyInfo.SetValue(Model, newValue);
                    // ReSharper disable once ExplicitCallerInfoArgument
                    OnPropertyChanged(propertyName);
                }
            }
        }

        private void UpdateOrignalValue(object currentValue, string propertyName)
        {
            if (!_orignalValues.ContainsValue(propertyName))
            {
                _orignalValues.Add(propertyName,currentValue);
            }
        }

        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            return (TValue)propertyInfo.GetValue(Model);
        }

        protected TValue GetOrignalValue<TValue>(string propertyName)
        {
            return _orignalValues.ContainsKey(propertyName)
                ? (TValue)_orignalValues[propertyName]
                // ReSharper disable once ExplicitCallerInfoArgument
                : GetValue<TValue>(propertyName);
        }


        protected void RegisterCollection<TWrapper, TModel>(ObservableCollection<TWrapper> wrapperCollection,
                ICollection<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems.Cast<TWrapper>())
                    {
                        modelCollection.Remove(item.Model);
                    }
                    //As per ncrunch this condition is not hit because 
                    //notify property changed always working when property changed
                    if (e.NewItems != null)
                    {
                        foreach (var item in e.NewItems.Cast<TWrapper>())
                        {
                            modelCollection.Add(item.Model);
                        }
                    }
                }
            };
        }
    }
}