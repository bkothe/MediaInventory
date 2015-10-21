using System;
using MediaInventory.Infrastructure.Application;

namespace MediaInventory.Core
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
        public Artist Artist { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}