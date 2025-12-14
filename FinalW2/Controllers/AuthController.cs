using FLoanAPI.Domain.Models;
using LoanAPI.Application;
using LoanAPI.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FinalW2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILoggerService _logger; 

        public AuthController(IAuthService authService, ILoggerService logger)
        {
            _authService = authService;
            _logger = logger;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                await _logger.LogWarningAsync("Register attempt failed due to invalid model state.", null);
                return BadRequest(ModelState);
            }

            
            var newUser = new USER
            {
                FName = registerDto.FName!,
                LName = registerDto.LName!,
                Username = registerDto.Username!,
                Email = registerDto.Email!,
                Role = registerDto.Role,
                IsBlocked = false
            };

            var result = await _authService.RegisterAsync(newUser, registerDto.Password!);

            if (!result)
            {
                await _logger.LogWarningAsync($"Registration failed: Username {registerDto.Username} already exists.", null);
                return BadRequest(new { Message = "Username already exists." });
            }

            await _logger.LogInfoAsync($"User registered successfully: {registerDto.Username}", newUser.ID);
            return StatusCode(201, new { Message = "Registration successful." });
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                await _logger.LogWarningAsync("Login attempt failed due to invalid model state.", null);
                return BadRequest(ModelState);
            }

            var (user, token) = await _authService.LoginAsync(loginDto.Username!, loginDto.Password!);

            if (user == null || token == null)
            {
                await _logger.LogWarningAsync($"Login failed for user: {loginDto.Username}.", null);
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            await _logger.LogInfoAsync($"User logged in successfully: {user.Username}", user.ID);

            
            return Ok(new
            {
                Token = token,
                Username = user.Username,
                Role = user.Role.ToString()
            });
        }
    }
}