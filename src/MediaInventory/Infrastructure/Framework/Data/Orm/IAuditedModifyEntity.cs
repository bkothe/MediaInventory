namespace MediaInventory.Infrastructure.Framework.Data.Orm
{
    public interface IAuditedModifyEntity
    {
        string ModifiedBy { get; set; }
    }
}