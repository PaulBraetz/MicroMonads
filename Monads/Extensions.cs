using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    internal static class Extensions
    {
        public static IEnumerable<T> GetEmptyIfDefault<T>(this IEnumerable<T> collection)
        {
            return collection ?? Array.Empty<T>();
        }
        public static Boolean IsEmptyOrDefault<T>(this IEnumerable<T> collection)
        {
            return !(collection?.Any() ?? false);
        }
    }
}
