using System;

namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface ITimestampedEntity
    {
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
    }
}