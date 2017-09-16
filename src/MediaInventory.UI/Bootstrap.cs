using MediaInventory.UI;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Bootstrap), "Start")]

namespace MediaInventory.UI
{
    public class Bootstrap
    {
        public static void Start()
        {

            //var configuration = GlobalConfiguration.Configuration;

            //configuration.InitializeGraphite(c => c
            //    .EnableDiagnosticsInDebugMode()
            //    .UseStructureMapContainer(configuration)
            //);

            //configuration.EnsureInitialized();

            //FubuApplication.For<Conventions>()
            //    .StructureMap(new Container(x => {
            //        x.AddRegistry<Application.Registry>();
            //        x.AddRegistry<Registry>();  
            //    }))
            //    .Packages(x =>
            //    {
            //        //x.Activator(new NonCachingVirtualPathProviderActivator());
            //        x.RemoveActivator<VirtualPathProviderActivator>();
            //    })
            //    .Bootstrap();

            //PackageRegistry.AssertNoFailures();
        }
    }
}