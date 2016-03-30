using FluentNHibernate.Mapping;
using MediaInventory.Core.Media;

namespace MediaInventory.Infrastructure.Application.Data.Persistence
{
    public class AudioMap : ClassMap<Audio>
    {
        public AudioMap()
        {
            Table("dbo.Media_Audio");

            DynamicInsert();
            DynamicUpdate();

            Id(x => x.Id).Column("MediaAudioId").GeneratedBy.GuidComb();

            Map(x => x.Title).Column("Title");
            Map(x => x.MediaFormat).Column("MediaFormatId").CustomType(typeof(MediaFormat));
            Map(x => x.Released).Column("Released").CustomType("datetime2").Precision(3);
            Map(x => x.Purchased).Column("Purchased").CustomType("datetime2").Precision(3);
            Map(x => x.PurchasePrice).Column("PurchasePrice");
            Map(x => x.PurchaseLocation).Column("PurchaseLocation");
            Map(x => x.MediaCount).Column("MediaCount").CustomSqlType("tinyint");
            Map(x => x.Notes).Column("Notes");
            Map(x => x.Created).Column("Created").CustomType("datetime2").Precision(3);
            Map(x => x.Modified).Column("Modified").CustomType("datetime2").Precision(3);

            References(x => x.Artist).Column("ArtistId");
        }
    }
}