using System;
using MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class Context
    {
        public Context(ILazySession session, Repositories repositories, Tracking tracking)
        {
            Session = session;
            Repositories = repositories;
            Tracking = tracking;
            Random = new Random();
        }

        public ILazySession Session { get; }
        public Repositories Repositories { get; }
        public Tracking Tracking { get; }
        public Random Random { get; private set; }

        public T CreateAndConfigure<T, TConfig>(Func<Repositories, T> create,
            Func<T, TConfig> target, Action<TConfig> config, Action<Tracking, T> track = null)
        {
            return CreateAndConfigure(create,
                config != null ? x => config(target(x)) : (Action<T>)null, track);
        }

        public T CreateAndConfigure<T>(Func<Repositories, T> create, Action<T> config, Action<Tracking, T> track = null)
        {
            var entity = create(Repositories);

            config?.Invoke(entity);

            Session.Flush();

            track?.Invoke(Tracking, entity);

            return entity;
        }
    }
}