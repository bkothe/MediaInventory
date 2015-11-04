using NHibernate.Cfg;

namespace MediaInventory.Infrastructure.Framework.Data.Orm.NHibernate
{
    public interface IEventListener
    {
        void Configure(Configuration config);
    }
}