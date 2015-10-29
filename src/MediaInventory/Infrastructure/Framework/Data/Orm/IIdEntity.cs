using System;

namespace MediaInventory.Infrastructure.Framework.Data.Orm
{
    public interface IIdEntity
    {
        Guid Id { get; set; }
    }
}