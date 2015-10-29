namespace MediaInventory.Infrastructure.Framework.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public const string NotFoundMessage = "{0} was not found";

        public NotFoundException(object key, string name) : base(NotFoundMessage.ToFormat(name))
        {
            Key = key;
            Name = name;
        }

        public string Name { get; set; }
        public object Key { get; set; }
    }
}