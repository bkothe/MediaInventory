using System;
using MediaInventory.Infrastructure.Framework.Data.Orm;

namespace MediaInventory.Core.Media
{
    public enum MediaFormat
    {
        CompactDisc = 1,
        Vinyl = 2,
        Dvd = 3,
        Vhs = 4,
        HdDvd = 5,
        BluRay = 6,
        Mp3 = 7,
        Flac = 8,
        Shn = 9
    }

    public enum Rating
    {
        G = 1,
        Pg = 2,
        Pg13 = 3,
        R = 4,
        Nc17 = 5,
        X = 6,
        Nr = 7
    }

    public class CommercialAudioMedia : IMedia, IIdEntity, ITimestampedEntity
    {
        public virtual Guid Id { get; set; }
        public virtual Artist.Artist Artist { get; set; }
        public virtual string Title { get; set; }
        public virtual MediaFormat MediaFormat { get; set; }
        public virtual DateTime? Released { get; set; }
        public virtual DateTime? Purchased { get; set; }
        public virtual decimal? PurchasePrice { get; set; }
        public virtual string PurchaseLocation { get; set; }
        public virtual int MediaCount { get; set; }
        public virtual string Notes { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
    }

    public class VideoMedia : IMedia, IIdEntity, ITimestampedEntity
    {
        public Guid Id { get; set; }
        public Artist.Artist Artist { get; set; }
        public string Title { get; set; }
        public MediaFormat MediaFormat { get; set; }
        public DateTime? Released { get; set; }
        public DateTime? Purchased { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string PurchaseLocation { get; set; }
        public int MediaCount { get; set; }
        public string Notes { get; set; }
        public Rating Rating { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }

    public interface IMedia
    {
        string Title { get; set; }
        MediaFormat MediaFormat { get; set; }
        DateTime? Released { get; set; }
        DateTime? Purchased { get; set; }
        decimal? PurchasePrice { get; set; }
        string PurchaseLocation { get; set; }
        int MediaCount { get; set; }
        string Notes { get; set; }
    }
}