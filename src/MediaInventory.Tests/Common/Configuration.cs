using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bender.Reflection;

namespace MediaInventory.Tests.Common
{
    public class Configuration : Infrastructure.Application.Configuration.Configuration
    {
        private static readonly Lazy<Configuration> _configuration =
            new Lazy<Configuration>(() => SimpleConfig.Configuration.Load<Configuration>());

        public static Configuration Current => _configuration.Value;

        public static Infrastructure.Application.Configuration.Configuration Create()
        {
            var instance = Activator.CreateInstance(typeof(
                Infrastructure.Application.Configuration.Configuration));

            Traverse(instance);

            return (Infrastructure.Application.Configuration.Configuration)instance;
        }

        private static void Traverse(object @object)
        {
            var properties = @object.GetType().GetPropertiesAndFields(
                typeof(IEnumerable<>), typeof(IEnumerable)).OfType<PropertyInfo>();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;

                if ((propertyType.IsList() && !propertyType.IsArray) || propertyType.IsListInterface())
                    property.SetValue(@object, propertyType.MakeConcreteGenericListType().CreateInstance(), null);
                else if (propertyType.IsClass && !propertyType.Namespace.StartsWith("System.") && (propertyType.GetConstructor(new Type[] { }) != null ||
                    propertyType.GetConstructor(new[] { @object.GetType() }) != null))
                {
                    var propertyValue = Activator.CreateInstance(propertyType,
                        propertyType.GetConstructor(new[] { @object.GetType() }) != null ? new[] { @object } : null);
                    property.SetValue(@object, propertyValue, null);
                    Traverse(propertyValue);
                }
            }
        }
    }
}