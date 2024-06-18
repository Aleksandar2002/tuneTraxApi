using API.Extensions;
using API.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private CreateJwtToken _createJwtToken;
        private ITokenStorage _storage;
        public AuthController(CreateJwtToken createJwtToken, ITokenStorage storage)
        {
            _createJwtToken = createJwtToken;
            _storage = storage;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserCredentials dto)
        {
            var token = _createJwtToken.MakeToken(dto.Email, dto.Password);
            return Ok(new { token });
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Logout()
        {

            _storage.Add(this.Request.GetTokenClaims().ToList());
            return NoContent();
        }
    }
}
