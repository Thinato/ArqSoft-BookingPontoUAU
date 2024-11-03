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
        GuestManager guestManager;
        Guest fakeGuest;
        [SetUp]
        public void Setup()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            fakeGuest = new Guest
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

            guestManager = new GuestManager(fakeRepo.Object, new Mapper(mapConfig));
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

            var res = await guestManager.CreateGuest(request);

            Console.WriteLine(res.Message);
            Console.WriteLine(res.ErrorCode);

            Assert.IsNotNull(res);
            // Assert.AreEqual(res.ErrorCode, ErrorCode.MISSING_REQUIRED_INFORMATION);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Name, guestDto.Name);
            Assert.AreEqual(res.Data.Surname, guestDto.Surname);
            Assert.AreEqual(res.Data.Email, guestDto.Email);
            Assert.AreEqual(res.Data.IdNumber, guestDto.IdNumber);
            Assert.AreEqual(res.Data.IdTypeCode, guestDto.IdTypeCode);
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

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message, "The passed ID is not valid");

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

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing passed required information");

        }

        [TestCase("emailsemarrobasemponto")]
        [TestCase("b@b.com")]

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

            var fakeRepo = new Mock<IGuestRepository>();

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.INVALID_EMAIL);
            Assert.AreEqual(res.Message, "The given email is not valid");

        }

        [Test]
        public async Task Should_Return_GuestNotFound_WhenDocsAreInvalid()
        {

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(null));

            var res = await guestManager.GetGuest(333);

            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.GUEST_NOT_FOUND);
            Assert.AreEqual(res.Message, "No guest record was found with the given id");

        }

        [Test]
        public async Task Should_Return_Guest_Success()
        {

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(fakeGuest));

            var res = await guestManager.GetGuest(333);

            Assert.IsNotNull(res);
            Assert.True(res.Success);

            Assert.AreEqual(res.Data.Id, fakeGuest.Id);
            Assert.AreEqual(res.Data.Name, fakeGuest.Name);

        }

    }
}