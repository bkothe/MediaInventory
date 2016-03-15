using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediaInventory.Infrastructure.Common.Reflection
{
    public static class AssemblyExttensions
    {
        public static bool IsInDebugMode(this Assembly assembly)
        {
            return assembly.GetCustomAttributes(typeof(DebuggableAttribute), false)
                .Cast<DebuggableAttribute>().Any(x => x.IsJITTrackingEnabled);
        }
    }
}
