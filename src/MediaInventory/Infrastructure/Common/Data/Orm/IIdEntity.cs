using System;

namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface IIdEntity
    {
        Guid Id { get; set; }
    }
}