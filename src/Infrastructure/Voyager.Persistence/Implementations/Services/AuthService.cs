using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;
using Voyager.Application.Abstraction.Repositories.IHotelManagerRepositories;
using Voyager.Application.Abstraction.Repositories.IVisitorRepositories;
using Voyager.Application.Abstraction.Services;
using Voyager.Application.DTOs.Auth_DTOs;
using Voyager.Application.DTOs.Response_DTOs;
using Voyager.Domain.Entities;
using Voyager.Domain.Identity;
using Voyager.Persistence.Exceptions;

namespace Voyager.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IVisitorWriteRepository _visitorWriteRepository;
        private readonly IHotelManagerWriteRepository _hotelManagerWriteRepository;

        public AuthService(IMapper mapper,
                           UserManager<AppUser> userManager,
                           IVisitorWriteRepository visitorWriteRepository,
                           ILogger<AuthService> logger,
                           IHotelManagerWriteRepository hotelManagerWriteRepository,
                           SignInManager<AppUser> signInManager,
                           IJwtService jwtService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _visitorWriteRepository = visitorWriteRepository;
            _logger = logger;
            _hotelManagerWriteRepository = hotelManagerWriteRepository;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }
        public async Task Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrEmpty(nameof(userRegisterDto));
              
                AppUser newUser = _mapper.Map<AppUser>(userRegisterDto);

                IdentityResult identityResult = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
                if (!identityResult.Succeeded) { HandleIdentityErrors(identityResult); }

                var roleResult = await _userManager.AddToRoleAsync(newUser, userRegisterDto.Role.ToString());
                if (!roleResult.Succeeded) { HandleIdentityErrors(roleResult); }

                await HandleRegistrationByRole(newUser, userRegisterDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Registration failed for {userRegisterDto.Email}: {ex.Message}");
                throw;
            }
        }
        public async Task<TokenResponseDto> Login(UserSignInDto userSignInDto)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(userSignInDto));

            AppUser user = await _userManager.FindByEmailAsync(userSignInDto.Email) ?? throw new ArgumentException();
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, userSignInDto.Password, true);

            if (!signInResult.Succeeded) { _logger.LogError($"Failed to login Email: {userSignInDto.Email} Password: {userSignInDto.Password}"); throw new SignInException("Failed to Sign in");  }

            return await TokenGenerator(user);
        }
        private async Task HandleRegistrationByRole(AppUser newUser, UserRegisterDto userRegisterDto)
        {
            switch (userRegisterDto.Role)
            {
                case Domain.Enums.Roles.Visitor:
                    await HandleVisitor(newUser, userRegisterDto);
                    break;
                case Domain.Enums.Roles.HotelManager:
                    await HandleHotelManager(newUser, userRegisterDto);
                    break;
            }
        }
        private void HandleIdentityErrors(IdentityResult identityResult)
        {
            StringBuilder errorMessage = new();
            foreach (var error in identityResult.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new AccountException(errorMessage.ToString());
        }
        private async Task HandleVisitor(AppUser visitor, UserRegisterDto userRegisterDto)
        {
            Visitor newVisitor = _mapper.Map<Visitor>(userRegisterDto);
            newVisitor.AppUser = visitor;
            await _visitorWriteRepository.AddAsync(newVisitor);
            await _visitorWriteRepository.SaveChangeAsync();
        }
        private async Task HandleHotelManager(AppUser manager, UserRegisterDto userRegisterDto)
        {
            HotelManager newManager = _mapper.Map<HotelManager>(userRegisterDto);
            newManager.AppUser = manager;
            await _hotelManagerWriteRepository.AddAsync(newManager);
            await _hotelManagerWriteRepository.SaveChangeAsync();
        }
        private async Task<TokenResponseDto> TokenGenerator(AppUser user)
        {
            TokenResponseDto tokenResponseDto = await _jwtService.CreateJwt(user);
            user.RefreshToken = tokenResponseDto?.RefreshToken;
            user.RefreshTokenExpiration = tokenResponseDto?.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);
            return tokenResponseDto;
        }

    }
}
