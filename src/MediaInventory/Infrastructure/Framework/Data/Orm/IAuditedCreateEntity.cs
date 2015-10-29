namespace MediaInventory.Infrastructure.Framework.Data.Orm
{
    public interface IAuditedCreateEntity
    {
        string CreatedBy { get; set; }
    }
}