using Application.Guests;
using Data.Pagination;
using Domain.Rooms.Entities;
using Domain.Rooms.Enums;
using Domain.Rooms.Ports;
using Domain.Rooms.ValueObjects;
using Moq;
using Shared.Pagination;

namespace ApplicationTest
{
    public class PaginationTests
    {
        private PaginationService<Room> _paginationService;
        private IQueryable<Room> _mockedQuery;

        [SetUp]
        public void SetUp()
        {
            _mockedQuery = Enumerable.Range(1, 100).ToArray()
                    .Select(i => new Room()
                        {
                            Id = i,
                            Name = $"Room n° {i}",
                            Level = 1,
                            Price = new Price(100, AcceptedCurrencies.DOLLAR),
                            HasGuest = false,
                            InMaintenance = false
                        })
                    .AsQueryable();
            
            _paginationService = new PaginationService<Room>();
        }

        [TestCase(1, 10)]
        [TestCase(1, 20)]
        [TestCase(3, 20)]
        public async Task HappyPath(int page, int count)
        {
            var options = new PaginationQuery(page, count).ToOptions();
            var data = await _paginationService.Paginate(_mockedQuery, options);

            int firstId = (page - 1) * count + 1;
            double totalPages = Math.Ceiling(_mockedQuery.Count() / (double)count);

            Assert.Multiple(() =>
            {
                Assert.That(data.Item1.First().Id, Is.EqualTo(firstId));
                Assert.That(data.Item2.Items, Is.EqualTo(count));
                Assert.That(data.Item2.CurrentPage, Is.EqualTo(page));
                Assert.That(data.Item2.TotalPages, Is.EqualTo(totalPages));
            });
        }

        [TestCase(1, 200)]
        public async Task ShouldThrowPaginationOffset(int page, int count)
        {
            var options = new PaginationQuery(page, count).ToOptions();

            Assert.Throws<PaginationOffsetException>(async () =>
                    await _paginationService.Paginate(_mockedQuery, options));
        }
    }
}