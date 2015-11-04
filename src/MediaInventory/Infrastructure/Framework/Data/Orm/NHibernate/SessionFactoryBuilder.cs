using System.Collections.Generic;
using System.Data;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using MediaInventory.Core;
using NHibernate;
using NHibernate.Util;
using MediaInventory.Infrastructure.Application.Configuration;

namespace MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory Build();
    }

    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        private readonly Configuration _configuration;
        private readonly IEnumerable<IInterceptor> _interceptors;
        private readonly IEnumerable<IConvention> _conventions;
        private readonly IEnumerable<IEventListener> _eventListeners;

        public SessionFactoryBuilder(
            Configuration configuration,
            IEnumerable<IInterceptor> interceptors,
            IEnumerable<IConvention> conventions,
            IEnumerable<IEventListener> eventListeners)
        {
            _configuration = configuration;
            _interceptors = interceptors;
            _conventions = conventions;
            _eventListeners = eventListeners;
        }

        public ISessionFactory Build()
        {
            return Fluently.Configure()
                .Sql2012Database(_configuration.Data.MediaInventory.ConnectionString, IsolationLevel.ReadCommitted, false)
                .Mappings(map =>
                {
                    map.FluentMappings.AddFromAssemblyOf<Registry>()
                        .Conventions.Add(AutoImport.Never());
                    _conventions.ForEach(x => map.FluentMappings.Conventions.Add(x));
                })
                .ExposeConfiguration(config =>
                {
                    _eventListeners.ForEach(x => x.Configure(config));
                    config.SetInterceptors(_interceptors.ToArray())
                        .AutoQuote()
                        .CommandTimeout(120);
                }).
                //HibernatingRhinosProfiler(Assembly.GetExecutingAssembly().IsInDebugMode()).
                BuildConfiguration().
                BuildSessionFactory();
        }
    }
}