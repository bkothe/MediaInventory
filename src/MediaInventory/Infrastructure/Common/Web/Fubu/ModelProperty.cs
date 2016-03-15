using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediaInventory.Infrastructure.Common.Web.Fubu
{
    public class ModelProperty
    {
        private readonly static Func<Type, PropertyInfo> GetModelProperty = x => x
                .GetProperties()
                .FirstOrDefault(y => (y.Name == "Model" && !y.PropertyType.IsPrimitive) ||
                                     (y.Name == "Models" && IsListType(y)));

        private readonly Type _listType;
        private readonly MethodInfo _listAddItem;
        private readonly PropertyInfo _property;

        private ModelProperty(PropertyInfo property)
        {
            _property = property;
            IsList = IsListType(property);
            PropertyType = property.PropertyType;
            if (IsList)
            {
                ModelType = property.PropertyType.GetGenericArguments()[0];
                _listType = typeof(List<>).MakeGenericType(ModelType);
                _listAddItem = _listType.GetMethod("Add");
            }
            else ModelType = property.PropertyType;
        }

        public static bool HasModelProperty(Type type)
        {
            return GetModelProperty(type) != null;
        }

        public static ModelProperty Create(Type type)
        {
            var property = GetModelProperty(type);
            return property == null ? null : new ModelProperty(property);
        }

        public Type PropertyType { get; private set; }
        public Type ModelType { get; }
        public bool IsList { get; }

        public void AddModel(object instance, object value)
        {
            if (!IsList) throw new Exception("Property must be a List<T> or IList<T>.");
            var list = _property.GetValue(instance, null);
            if (list == null)
            {
                list = Activator.CreateInstance(_listType);
                _property.SetValue(instance, list, null);
            }
            _listAddItem.Invoke(list, new[] { value });
        }

        public void SetValue(object instance, object value)
        {
            _property.SetValue(instance, value, null);
        }

        private static bool IsListType(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType &&
                   new[] { typeof(List<>), typeof(IList<>) }.Contains(property.PropertyType.GetGenericTypeDefinition());
        }
    }
}
