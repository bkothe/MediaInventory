namespace MediaInventory.Infrastructure.Framework.Data.Orm
{
    public interface IAuditedEntity
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}