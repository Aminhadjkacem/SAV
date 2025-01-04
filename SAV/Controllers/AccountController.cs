using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SAV.Models;
using SAV.Services.Interface;

namespace SAV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AccountController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        // POST: api/Account/Signup
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (identityResult, clientId) = await _loginService.Signup(model);

            if (!identityResult.Succeeded)
                return BadRequest(identityResult.Errors);

            // Include `clientId` only if the role is `CLIENT`
            var response = new
            {
                message = "User created successfully",
                role = model.Role,
                clientId = model.Role == "CLIENT" ? clientId : null
            };

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (signInResult, user, roles, clientId) = await _loginService.LoginAsync(model);

            if (!signInResult.Succeeded)
                return Unauthorized("Invalid email or password.");

            // Check the role and include `clientId` only if the role is `CLIENT`
            var response = new
            {
                message = "Login successful",
                userName = user.UserName,
                roles = roles,
                clientId = roles.Contains("CLIENT") ? clientId : null
            };

            return Ok(response);
        }


    }
}
