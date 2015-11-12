using System;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Venue
{
    public class Venue : IIdEntity, ITimestampedEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual Venue PreviousVenue { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }

        public virtual Venue Clone()
        {
            return (Venue)MemberwiseClone();
        }
    }
}