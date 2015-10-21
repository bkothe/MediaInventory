using System;

namespace MediaInventory.Infrastructure.Application
{
    public interface ITimestampedEntity
    {
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
    }
}
