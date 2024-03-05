using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.multitracks.com.MappingExtensions
{
    public static class EFExtensions
    {
        public static Task<List<TSource>> ToListAsyncSafe<TSource>(
            this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!(source is IAsyncEnumerable<TSource>))
                return Task.FromResult(source.ToList());
            return source.ToListAsync();
        }
    }
}
