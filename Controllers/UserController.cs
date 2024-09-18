using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_app_backend.AppDataContext;
using social_app_backend.DTOs;
using social_app_backend.Models;
using social_app_backend.Services;
using social_app_backend.Utils;

namespace social_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly ILogger<UserServices> _logger;

        public UserController(UserServices userServices, ILogger<UserServices> logger)
        {
            _userServices = userServices;
            _logger = logger;
        }
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO request)
        {
            try
            {
                var user = await _userServices.CreateUserAsync(request);
                return Ok(new { data = user, success = true });
            }
            catch (ServiceException e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(e.StatusCode, new { data = e.Message, success = false });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new { data = e.Message, success = false });
            }
        }
        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<IActionResult> Login(LoginUserDTO request)
        {
            try
            {
                User? user = await _userServices.GetUserAsync(user => user.Email == request.Email && user.Password == request.Password, new ServiceOptions
                {
                    ThrowErrorIfNotExists = true
                });
                return Ok(new { data = user, success = true });
            }
            catch (ServiceException e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(e.StatusCode, new { data = e.Message, success = false });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new { data = e.Message, success = false });
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                List<User> users = await _userServices.GetAllUsersAsync();
                return Ok(new { success = true, data = users });
            }
            catch (ServiceException e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(e.StatusCode, new { data = e.Message, success = false });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new { data = e.Message, success = false });
            }
        }
    }
}
