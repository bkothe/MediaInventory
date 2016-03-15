using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
