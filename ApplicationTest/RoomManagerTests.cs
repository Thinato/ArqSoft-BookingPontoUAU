using Application.Rooms;
using Application.Rooms.Requests;
using Data.Rooms;
using Domain.Rooms.Entities;
using Domain.Rooms.Enums;
using Domain.Rooms.Ports;
using Domain.Rooms.ValueObjects;
using Moq;

namespace ApplicationTest
{
    public class RoomManagerTests
    {
        private IEnumerable<Room> _fakeRooms;
        private Mock<IRoomRepository> _fakeRepository;
        private RoomManager _roomManager;

        [SetUp]
        public void SetUp()
        {
            _fakeRooms = Enumerable.Range(1, 10)
                    .Select(i => new Room()
                        {
                            Id = i,
                            Name = $"Room {i}",
                            Level = i,
                            Price = new Price(i * 50, AcceptedCurrencies.EURO),
                            HasGuest = false,
                            InMaintenance = false,
                        })
                    .ToList();

            _fakeRepository = new Mock<IRoomRepository>();

            _roomManager = new RoomManager(_fakeRepository.Object);
        }

        [TestCase("VIP Room", 10, 3999)]
        public async Task ShouldCreateWithSuccess(
                string name,
                int level,
                int value)
        {
            var request = new CreateRoomRequest()
            {
                Name = name,
                Level = level,
                Currency = "DOLLAR",
                Value = value
            };

            var response = await _roomManager.Create(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.Data.Id, Is.EqualTo(10));
                Assert.That(response.Data.Name, Is.EqualTo(name));
                Assert.That(response.Data.Level, Is.EqualTo(level));
                Assert.That(response.Data.Price.Value, Is.EqualTo(value));
            });
        }

        public async Task ShouldGetOneWithSuccess()
        {
            _fakeRepository.Setup(r => r.GetRoom(1))
                    .Returns(Task.FromResult<Room?>(_fakeRooms.First()));
            
            var response = await _roomManager.GetRoom(1);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.Not.Null);
            Assert.Multiple(() => {
                Assert.That(response.Data.Id, Is.EqualTo(1));
                Assert.That(response.Data.Name, Is.EqualTo("Room 1"));
            });
        }
    }
}