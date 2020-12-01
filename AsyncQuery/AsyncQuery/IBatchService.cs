using System.Collections.Generic;
using System.Linq;

namespace AsyncQuery
{
    public interface IBatchService
    {
        IEnumerable<List<T>> Batch<T>(IOrderedQueryable<T> collection, int size);
        IAsyncEnumerable<List<T>> BatchAsync<T>(IOrderedQueryable<T> collection, int size);
    }
}