using FluentNHibernate.Mapping;
using MediaInventory.Core.Performance;

namespace MediaInventory.Infrastructure.Application.Persistence
{
    public class ConcertMap : ClassMap<Concert>
    {
        public ConcertMap()
        {
            Table("dbo.Concert");

            DynamicInsert();
            DynamicUpdate();

            Id(x => x.Id).Column("ConcertId").GeneratedBy.GuidComb();

            Map(x => x.Date).Column("ConcertDate").CustomType("datetime2").Precision(3);

            Map(x => x.Created).Column("Created").CustomType("datetime2").Precision(3);
            Map(x => x.Modified).Column("Modified").CustomType("datetime2").Precision(3);

            References(x => x.Artist).Column("ArtistId");
            References(x => x.Venue).Column("VenueId");
        }
    }
}