﻿using MediaInventory.Infrastructure.Application.Configuration;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Data.Orm.NHibernate;

namespace MediaInventory.Application
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            ForSingletonOf<Configuration>()
                .Use(SimpleConfig.Configuration.Load<Configuration>());

            // ------------------------------ Data Access ------------------------------
            // NHibernate
            ForSingletonOf<ISessionFactoryBuilder>().Use<SessionFactoryBuilder>();
            ForSingletonOf<ILazySessionFactory>().Use<LazySessionFactory>();

            For<ILazySession>().Use(context => new LazySession(context.GetInstance<ILazySessionFactory>()));
            For<IUnitOfWork>().Use<UnitOfWork>();
            For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }
    }
}