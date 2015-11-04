using MediaInventory.Infrastructure.Application.Configuration;

namespace MediaInventory.Core
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            var configuration = SimpleConfig.Configuration.Load<Configuration>();

            ForSingletonOf<Configuration>().Use(configuration);
        }
    }
}
