using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Voyager.Application.Abstraction.Services;
using Voyager.Application.DTOs.Auth_DTOs;
using Voyager.Domain.Identity;

namespace Voyager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(IAuthService authService,
                                  SignInManager<AppUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            await _authService.Register(userRegisterDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserSignInDto userSignInDto)
        {
            var response = await _authService.Login(userSignInDto);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
