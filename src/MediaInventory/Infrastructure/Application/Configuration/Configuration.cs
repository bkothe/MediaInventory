using System.Xml.Serialization;

namespace MediaInventory.Infrastructure.Application.Configuration
{
    [XmlType("mediaInventory")]
    public class Configuration
    {
        public Data Data { get; set; }
        public Web Web { get; set; }
    }
}