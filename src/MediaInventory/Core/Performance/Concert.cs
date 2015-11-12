using System;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Performance
{
    public enum PerformanceType
    {
    }

    public interface IPerformance
    {
        DateTime Date { get; set; }
    }

    public class Concert : IIdEntity, IPerformance, ITimestampedEntity
    {
        public virtual Guid Id { get; set; }
        public virtual Artist.Artist Artist { get; set; }
        public virtual Venue.Venue Venue { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }

        public virtual Concert Clone()
        {
            return (Concert)MemberwiseClone();
        }
    }
}