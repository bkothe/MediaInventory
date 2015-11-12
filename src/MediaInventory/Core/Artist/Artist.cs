using System;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Artist
{
    public class Artist : IIdEntity, ITimestampedEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }

        public virtual Artist Clone()
        {
            return (Artist)MemberwiseClone();
        }
    }
}