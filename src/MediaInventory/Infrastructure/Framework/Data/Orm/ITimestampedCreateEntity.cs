using System;

namespace MediaInventory.Infrastructure.Framework.Data.Orm
{
    public interface ITimestampedCreateEntity
    {
        DateTime Created { get; set; }
    }
}