using AgendaApi.Helper;
using AgendaApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        private IEnumerable<User> logins = new List<User>() {
            new User() {
                    Id = Guid.NewGuid(),
                    EmailId = "adminakp@gmail.com",
                    UserName = "Admin",
                    Password = "Admin",
                },
                new User() {
                    Id = Guid.NewGuid(),
                    EmailId = "adminakp@gmail.com",
                    UserName = "User1",
                    Password = "Admin",
                }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var token = new UserTokens();
                var Valid = logins.Any(x => x.UserName.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = logins.FirstOrDefault(x => x.UserName.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    token = JwtHelper.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.EmailId,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Id = user.Id,
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("wrong password");
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetList()
        {
            return Ok(logins);
        }
    }
}
