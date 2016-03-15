using System;
using System.Linq;
using System.Reflection;
using MediaInventory.Infrastructure.Common.Linq;

namespace MediaInventory.Infrastructure.Common.Objects
{
    public static class ModelMapping
    {
        public static void Bootstrap<T>()
        {
            Bootstrap(typeof(T).Assembly);
        }

        public static void Bootstrap(Assembly assembly = null)
        {
            (assembly ?? Assembly.GetCallingAssembly()).GetTypes()
                .Where(x => typeof(IModelMapping).IsAssignableFrom(x) && !x.IsInterface)
                .ToList().ForEach(x => Activator.CreateInstance(x).As<IModelMapping>().Initialize());
        }
    }
}