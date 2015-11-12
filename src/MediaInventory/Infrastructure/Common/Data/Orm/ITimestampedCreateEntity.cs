using System;

namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface ITimestampedCreateEntity
    {
        DateTime Created { get; set; }
    }
}