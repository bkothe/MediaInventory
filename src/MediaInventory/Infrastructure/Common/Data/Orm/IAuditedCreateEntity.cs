namespace MediaInventory.Infrastructure.Common.Data.Orm
{
    public interface IAuditedCreateEntity
    {
        string CreatedBy { get; set; }
    }
}