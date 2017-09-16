using System.Diagnostics;
using System.Linq;
using System.Reflection;

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
