using System;

namespace MediaInventory.Infrastructure.Framework.Data.Orm
{
    public interface ITimestampedModifyEntity
    {
        DateTime? Modified { get; set; }
    }
}