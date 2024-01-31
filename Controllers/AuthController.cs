using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POLL.API.Models;
using POLL.API.Services;

namespace POLL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                // Validate model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the UserService to register a new user
                var registeredUser = await _userService.RegisterAsync(user);

                // Return a success response with the registered user
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                // Validate model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the UserService to perform user login
                var loggedInUser = await _userService.LoginAsync(user);

                // Check if login was successful
                if (loggedInUser == null)
                {
                    return Unauthorized("Invalid username or password");
                }

                // Generate a JWT token
                var token = _jwtService.GenerateToken(loggedInUser);

                // Return the token along with any additional user data
                return Ok(new { Token = token, User = loggedInUser });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
