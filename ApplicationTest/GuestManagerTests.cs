using Application;
using Application.Guests;
using Application.Guests.Dtos;
using Application.Guests.Requests;
using AutoMapper;
using Domain.Guests.Entities;
using Domain.Guests.Enums;
using Domain.Guests.Ports;
using Domain.Guests.ValueObjects;
using Moq;

namespace ApplicationTest
{
    public class Tests
    {
        private GuestManager _guestManager;
        private Guest _fakeGuest;
        private Mock<IGuestRepository> _fakeRepository;
        
        [SetUp]
        public void Setup()
        {
            _fakeRepository = new Mock<IGuestRepository>();

            _fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test",
                DocumentId = new PersonId
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123"
                }
            };

            var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GuestMapping>();
                });

            _guestManager = new GuestManager(_fakeRepository.Object, new Mapper(mapConfig));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task HappyPath()
        {
            var guestDto = new GuestDto
            {

                Name = "Fulano",
                Surname = "De tal",
                Email = "fulano@email.com",
                IdNumber = "abcd",
                IdTypeCode = 1,
            };


            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var res = await _guestManager.CreateGuest(request);

            Assert.That(res, Is.Not.Null);
            Assert.Multiple(() =>
            {
                // Assert.AreEqual(res.ErrorCode, ErrorCode.MISSING_REQUIRED_INFORMATION);
                Assert.That(res.Success, Is.True);
                Assert.That(guestDto.Name, Is.EqualTo(res.Data.Name));
                Assert.That(guestDto.Surname, Is.EqualTo(res.Data.Surname));
                Assert.That(guestDto.Email, Is.EqualTo(res.Data.Email));
                Assert.That(guestDto.IdNumber, Is.EqualTo(res.Data.IdNumber));
                Assert.That(guestDto.IdTypeCode, Is.EqualTo(res.Data.IdTypeCode));
            });
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDto
            {
                Name = "Fulano",
                Surname = "De tal",
                Email = "fulano@email.com",
                IdNumber = docNumber,
                IdTypeCode = 1,
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var res = await _guestManager.CreateGuest(request);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Success, Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCode.INVALID_PERSON_ID));
                Assert.That(res.Message, Is.EqualTo("The passed ID is not valid"));
            });
        }

        [TestCase("", "Surname teste", "email@email.com")]
        [TestCase("Name", "", "email@email.com")]
        [TestCase("Name", "Surname teste", "")]
        [TestCase("", "", "")]

        public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(
                string name,
                string surname,
                string email)
        {
            var guestDto = new GuestDto
            {

                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1,
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            var res = await _guestManager.CreateGuest(request);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Success, Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCode.MISSING_REQUIRED_INFORMATION));
                Assert.That(res.Message, Is.EqualTo("Missing passed required information"));
            });
        }

        [TestCase("emailsemarrobasemponto")]
        public async Task Should_Return_InvalidEmailException_WhenDocsAreInvalid(string email)
        {
            var guestDto = new GuestDto
            {
                Name = "Fulano",
                Surname = "De tal",
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1,
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var res = await _guestManager.CreateGuest(request);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Success, Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCode.INVALID_EMAIL));
                Assert.That(res.Message, Is.EqualTo("The given email is not valid"));
            });
        }

        [Test]
        public async Task Should_Return_GuestNotFound_WhenDocsAreInvalid()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(null));

            var res = await _guestManager.GetGuest(333);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Success, Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCode.GUEST_NOT_FOUND));
                Assert.That(res.Message, Is.EqualTo("No guest record was found with the given id"));
            });
        }

        [Test]
        public async Task Should_Return_Guest_Success()
        {
            _fakeRepository.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(_fakeGuest));

            var res = await _guestManager.GetGuest(333);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Success, Is.True);
            Assert.Multiple(() =>
            {
                Assert.That(_fakeGuest.Id, Is.EqualTo(res.Data.Id));
                Assert.That(_fakeGuest.Name, Is.EqualTo(res.Data.Name));
            });
        }
    }
}