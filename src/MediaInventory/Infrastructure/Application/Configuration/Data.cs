namespace MediaInventory.Infrastructure.Application.Configuration
{
    public class Data
    {
        public ConnectionStrings MediaInventory { get; set; }
    }

    public class ConnectionStrings
    {
        public string ConnectionString { get; set; }
    }
}