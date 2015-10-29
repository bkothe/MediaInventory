using System;

namespace MediaInventory.Infrastructure.Framework.Data.Orm
{
    public interface ITimestampedEntity
    {
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
    }
}