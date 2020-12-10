using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Base.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }
    }
}
