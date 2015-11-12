using NHibernate.Cfg;

namespace MediaInventory.Infrastructure.Common.Data.Orm.NHibernate
{
    public interface IEventListener
    {
        void Configure(Configuration config);
    }
}