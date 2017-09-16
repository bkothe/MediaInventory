namespace MediaInventory.Infrastructure.Common.Linq
{
    public static class LinqExtensions
    {
        public static T As<T>(this object source)
        {
            return source == null ? default(T) : (T)source;
        }
    }
}
