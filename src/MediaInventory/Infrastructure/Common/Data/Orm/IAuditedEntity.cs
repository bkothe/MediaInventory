namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface IAuditedEntity
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}