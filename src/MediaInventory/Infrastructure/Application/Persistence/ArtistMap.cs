using FluentNHibernate.Mapping;
using MediaInventory.Core.Artist;

namespace MediaInventory.Infrastructure.Application.Persistence
{
    public class ArtistMap : ClassMap<Artist>
    {
        public ArtistMap()
        {
            Table("dbo.Artist");

            DynamicInsert();
            DynamicUpdate();

            Id(x => x.Id).Column("ArtistId").GeneratedBy.GuidComb();

            Map(x => x.Name).Column("Name");
            Map(x => x.Created).Column("Created").CustomType("datetime2").Precision(3);
            Map(x => x.Modified).Column("Modified").CustomType("datetime2").Precision(3);
        }
    }
}