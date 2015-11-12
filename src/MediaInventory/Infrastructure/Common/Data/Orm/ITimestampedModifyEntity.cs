using System;

namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface ITimestampedModifyEntity
    {
        DateTime? Modified { get; set; }
    }
}