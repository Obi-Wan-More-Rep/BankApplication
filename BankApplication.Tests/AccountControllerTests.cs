using AutoMapper;
using BankApplication.Controllers;
using BankApplication.Core.Interfaces;
using BankApplication.Data.Interfaces;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;
using BankApplication.Domain.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace BankApplication.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<IAccountService> _accountServiceMock;
        private AccountController _accountController;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize mocks
            _accountServiceMock = new Mock<IAccountService>();

            // Use the real AutoMapper configuration
            var mapper = ConfigureMapper();

            var loggerMock = new Mock<ILogger<AccountController>>();

            // Initialize the controller with mocks
            _accountController = new AccountController(_accountServiceMock.Object, mapper, loggerMock.Object);

            // Mock the User property used in claims in the controller
            var customerId = 1;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, customerId.ToString()),
            }));

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        // Configure IMapper
        private IMapper ConfigureMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountCreationProfile>();
            });

            return configuration.CreateMapper();
        }

        [TestMethod]
        public async Task CreateAccount_ValidData_ReturnsCreated()
        {
            // Arrange
            var accountCreationDto = new AccountCreationDTO
            {
                Frequency = "Monthly",
                AccountTypesId = 1
            };

            // Mock the CustomerService method CreateCustomerAccountAsync
            _accountServiceMock.Setup(service => service.CreateCustomerAccountAsync(It.IsAny<int>(), It.IsAny<Account>()));

            // Act
            var result = await _accountController.CreateAccountAsync(accountCreationDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)result).StatusCode, 201);
        }

        [TestMethod]
        public async Task CreateAccount_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var accountCreationDto = new AccountCreationDTO
            {
                // missing properties to make a BadRequest return
            };

            // Act
            var result = await _accountController.CreateAccountAsync(accountCreationDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(((BadRequestObjectResult)result).StatusCode, 400);
        }

        [TestMethod]
        public async Task CreateAccount_RepositoryException_ReturnsInternalServerError()
        {
            // Arrange
            var accountCreationDto = new AccountCreationDTO
            {
                Frequency = "Monthly",
                AccountTypesId = 1
            };

            // Mock the Accountservice method CreateCustomerAccounntAsync to throw an exception
            _accountServiceMock.Setup(service => service.CreateCustomerAccountAsync(It.IsAny<int>(), It.IsAny<Account>()))
                            .Throws(new Exception("Made up exception"));

            // Act
            var result = await _accountController.CreateAccountAsync(accountCreationDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)result).StatusCode, 500);
        }
    }
}