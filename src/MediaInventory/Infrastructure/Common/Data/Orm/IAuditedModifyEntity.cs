namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface IAuditedModifyEntity
    {
        string ModifiedBy { get; set; }
    }
}