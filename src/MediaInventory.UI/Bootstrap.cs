using Bottles;
using FubuMVC.Core;
using FubuMVC.Core.Packaging.VirtualPaths;
using FubuMVC.StructureMap3;
using MediaInventory.Infrastructure.Common.Objects;
using MediaInventory.Infrastructure.Common.Web.Fubu;
using MediaInventory.UI;
using StructureMap;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Bootstrap), "Start")]

namespace MediaInventory.UI
{
    public class Bootstrap
    {
        public static void Start()
        {

            //DynamicModuleUtility.RegisterModule(typeof(ServerHeaderModule));

            FubuApplication.For<Conventions>()
                .StructureMap(new Container(x => {
                    x.AddRegistry<Application.Registry>();
                    x.AddRegistry<Registry>();  
                }))
                .Packages(x =>
                {
                    //x.Activator(new NonCachingVirtualPathProviderActivator());
                    x.RemoveActivator<VirtualPathProviderActivator>();
                })
                .Bootstrap();

            PackageRegistry.AssertNoFailures();

            ModelMapping.Bootstrap();
        }
    }
}