using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_app_backend.DTOs;
using social_app_backend.Utils;

namespace social_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestDTO request) {
            string? userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null) {
                throw new ServiceException("User Id not found!", 403);
            }
            int id = int.Parse(userId);
            

        }
    }
}
