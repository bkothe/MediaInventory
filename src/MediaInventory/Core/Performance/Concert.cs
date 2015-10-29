using System;
using MediaInventory.Infrastructure.Application;

namespace MediaInventory.Core.Performance
{
    public enum PerformanceType
    {
    }

    public interface IPerformance
    {
        DateTime Date { get; set; }
    }

    public class Concert : IPerformance, ITimestampedEntity
    {
        public Guid Id { get; set; }
        public Artist.Artist Artist { get; set; }
        public Venue.Venue Venue { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Concert Clone()
        {
            return (Concert)MemberwiseClone();
        }
    }
}