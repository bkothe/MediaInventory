using FluentNHibernate.Mapping;
using MediaInventory.Core.Administration;

namespace MediaInventory.Infrastructure.Application.Persistence
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("dbo.[User]");

            DynamicInsert();
            DynamicUpdate();

            Id(x => x.Id).Column("UserId").GeneratedBy.GuidComb();

            Map(x => x.IsActive).Column("Active");
            Map(x => x.FirstName).Column("FirstName");
            Map(x => x.LastName).Column("LastName");
            Map(x => x.EmailAddress).Column("EmailAddress");
            Map(x => x.Password).Column("Password");
            Map(x => x.LastLogin).Column("LastLogin").CustomType("datetime2").Precision(3);
            Map(x => x.Created).Column("Created").CustomType("datetime2").Precision(3);
            Map(x => x.Modified).Column("Modified").CustomType("datetime2").Precision(3);
        }
    }
}