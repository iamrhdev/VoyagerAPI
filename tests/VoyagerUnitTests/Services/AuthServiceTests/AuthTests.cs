using Moq;
using Voyager.Application.Abstraction.Repositories.IVisitorRepositories;
using Voyager.Application.Abstraction.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Voyager.Application.Abstraction.Repositories.IHotelManagerRepositories;
using Voyager.Domain.Identity;
using Voyager.Persistence.Implementations.Services;
using AutoMapper;
using Voyager.Application.DTOs.Auth_DTOs;
using Voyager.Domain.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Voyager.Domain.Entities;

namespace VoyagerUnitTests.Services.AuthServiceTests
{
    public class AuthTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ILogger<AuthService>> _loggerMock = new();
        private readonly Mock<UserManager<AppUser>> _userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        private readonly Mock<SignInManager<AppUser>> _signInManagerMock = new Mock<SignInManager<AppUser>>();
        private readonly Mock<IJwtService> _jwtServiceMock = new();
        private readonly Mock<IVisitorWriteRepository> _visitorWriteRepositoryMock = new();
        private readonly Mock<IHotelManagerWriteRepository> _hotelManagerWriteRepositoryMock = new Mock<IHotelManagerWriteRepository>();

        public AuthTests()
        {
            _signInManagerMock = new Mock<SignInManager<AppUser>>(
               _userManagerMock.Object,
               new HttpContextAccessor(),
               new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
               new Mock<IOptions<IdentityOptions>>().Object,
               new Mock<ILogger<SignInManager<AppUser>>>().Object,
               new Mock<IAuthenticationSchemeProvider>().Object,
               new Mock<IUserConfirmation<AppUser>>().Object);

            _authService = new AuthService(_mapperMock.Object,
                                           _userManagerMock.Object,
                                           _visitorWriteRepositoryMock.Object,
                                           _loggerMock.Object,
                                           _hotelManagerWriteRepositoryMock.Object,
                                           _signInManagerMock.Object,
                                           _jwtServiceMock.Object);
        }
        [Fact]
        public async Task Register_NullDto_MustThrowException()
        {
            // Arrange
            UserRegisterDto userRegisterDto = null;
            //Act and Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _authService.Register(userRegisterDto));
        }
        [Fact]
        public async Task Register_SuccessfulVisitorRegistration_CallsMethods()
        {
            // Arrange
            var userRegisterDto = new UserRegisterDto("Visitor", "Rahil","Habibli", "rhabibli@outlook.com", "Password123!", "Password123!", "+994102184232", Roles.Visitor);
            _userManagerMock.Setup(mock => mock.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(mock => mock.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _visitorWriteRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Visitor>()))
                .Returns(Task.FromResult(new Visitor()));

            // Act
            await _authService.Register(userRegisterDto);

            // Assert
            _userManagerMock.Verify(mock => mock.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Once);
            _userManagerMock.Verify(mock => mock.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Once);
            _visitorWriteRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Visitor>()), Times.Once);
        }
    }
}
