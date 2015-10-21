using System;
using MediaInventory.Infrastructure.Application;

namespace MediaInventory.Core
{
    public class Artist : ITimestampedEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
    }
}