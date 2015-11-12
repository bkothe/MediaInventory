using FluentNHibernate.Mapping;
using MediaInventory.Core.Venue;

namespace MediaInventory.Infrastructure.Application.Data.Persistence
{
    public class VenueMap : ClassMap<Venue>
    {
        public VenueMap()
        {
            Table("dbo.Venue");

            DynamicInsert();
            DynamicUpdate();

            Id(x => x.Id).Column("VenueId").GeneratedBy.GuidComb();

            Map(x => x.Name).Column("Name");
            Map(x => x.City).Column("City");
            Map(x => x.State).Column("State");

            Map(x => x.Created).Column("Created").CustomType("datetime2").Precision(3);
            Map(x => x.Modified).Column("Modified").CustomType("datetime2").Precision(3);

            References(x => x.PreviousVenue).Column("ParentVenueId").Nullable();
        }
    }
}
