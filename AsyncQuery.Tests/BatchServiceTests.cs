using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace AsyncQuery.Tests
{
    public class BatchServiceTests
    {
        private readonly IBatchService _batchService;

        public BatchServiceTests() => _batchService = new BatchService();

        [Fact]
        public void Should_batch()
        {
            // Arrange
            var actualCollection = new List<Item>();
            var expectedCollection = Enumerable.Range(0, 100).Select(x => new Item { Id = x }).ToList();
            var query = expectedCollection.AsQueryable().OrderBy(x => x.Id);

            // Act
            foreach (var items in _batchService.Batch(query, 10))
            {
                actualCollection.AddRange(items);
            }

            // Assert
            actualCollection.Should().BeEquivalentTo(expectedCollection);
        }

        [Fact]
        public async Task Should_batch_async()
        {
            // Arrange
            var actualCollection = new List<Item>();
            var expectedCollection = Enumerable.Range(0, 100).Select(x => new Item { Id = x }).ToList(); // TODO: Mock this collection to pass this unit test
            var query = expectedCollection.AsQueryable().OrderBy(x => x.Id);

            // Act
            await foreach (var items in _batchService.BatchAsync(query, 10))
            {
                actualCollection.AddRange(items);
            }

            // Assert
            actualCollection.Should().BeEquivalentTo(expectedCollection);
        }
    }
}
