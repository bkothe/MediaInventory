using System;
using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using MediaInventory.Infrastructure.Application.Persistence;
using MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate;
using MediaInventory.Tests.Common.Fakes;

namespace MediaInventory.Tests.Common.Data
{
    public static class NHibernate
    {
        
        public static Func<ILazySession> OpenMediaInventorySession = () => new LazySession(MediaInventorySessionFactory);
        public static ILazySessionFactory MediaInventorySessionFactory = CreateSessionFactory(Configuration.Current.Data.MediaInventory.ConnectionString);

        public static ILazySessionFactory CreateSessionFactory(string connectionString)
        {
            return new LazySessionFactory(new MemorySessionFactoryBuilder(Fluently.Configure().
                Sql2012Database(connectionString, IsolationLevel.ReadCommitted, false).
                Mappings(map => map.FluentMappings.AddFromAssembly(typeof(ArtistMap).Assembly).Conventions.Add(AutoImport.Never())).
                ExposeConfiguration(config =>
                {
                    config.CommandTimeout(600)
                        .SetInterceptors()
                        .AutoQuote();
                }).
                //HibernatingRhinosProfiler(true).
                BuildConfiguration().
                BuildSessionFactory()));


            // password for MediaInventory user Fi4-HQKi10wwfSvrrxfe
        }
    }
}