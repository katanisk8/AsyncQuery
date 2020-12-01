using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AsyncQuery
{
    public class BatchService : IBatchService
    {
        public IEnumerable<List<T>> Batch<T>(IOrderedQueryable<T> collection, int size)
        {
            var batches = (double)collection.Count() / size;

            for (var i = 0; i < batches; i++)
            {
                yield return collection.Skip(i * size).Take(size).ToList();
            }
        }

        public async IAsyncEnumerable<List<T>> BatchAsync<T>(IOrderedQueryable<T> collection, int size)
        {
            var batches = (double)await collection.CountAsync() / size;

            for (var i = 0; i < batches; i++)
            {
                yield return await collection.Skip(i * size).Take(size).ToListAsync();
            }
        }
    }
}
