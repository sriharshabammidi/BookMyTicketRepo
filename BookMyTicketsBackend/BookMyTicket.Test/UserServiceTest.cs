using AutoMapper;
using BookMyTicket.BLL;
using BookMyTicket.Core.AutoMapperProfile;
using BookMyTicket.DAL;
using BookMyTicket.DAL.Configurations;
using BookMyTicket.Models;
using BookMyTicket.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookMyTicket.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly UserService userService;
        public UserServiceTest()
        {
            userService = CreateSearchService();
        }
        [TestMethod]
        public void RegisterUserAndLogin_Test_1()
        {
            UserProfile userProfile = new UserProfile()
            {
                FirstName = "First Name",
                LastName = "Last Name",
                Email = "test@mail.com",
                Password = "Test@123"
            };
            var isUserCreated = userService.AddUser(userProfile);
            Assert.IsTrue(isUserCreated);
            Assert.IsNotNull(userService.Login("test@mail.com", "Test@123"));
        }
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public void RegisterUserAndLogin_Test_2()
        {
            UserProfile userProfile = new UserProfile()
            {
                FirstName = "First Name",
                LastName = "Last Name",
                Email = "test@mail.com",
                Password = "Test@123"
            };
            var isUserCreated = userService.AddUser(userProfile);
            Assert.IsTrue(isUserCreated);
            userService.Login("test@mail.com", "WrongPassword");
        }
        private UserService CreateSearchService()
        {
            var options = new DbContextOptionsBuilder<BookMyTicketDBContext>()
                   .UseInMemoryDatabase(nameof(SearchServiceTest), new InMemoryDatabaseRoot())
                   .Options;
            var dbContext = new BookMyTicketDBContext(options);

            var userRepository = new UsersRepository(dbContext);
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var jwtIssuerOptions = new JwtIssuerOptions()
            {
                Audience = "https://localhost:44367/",
                Issuer = "BookMyTicketsWebApi"
            };
            var mapper = new Mapper(configuration);
            return new UserService(userRepository, jwtIssuerOptions, mapper);
        }
    }
}
