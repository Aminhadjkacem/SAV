using SAV.Models;
using Microsoft.AspNetCore.Identity;
using SAV.Services.Interface;
using Microsoft.EntityFrameworkCore;
using SAV.Data;

namespace SAV.Services.Impl
{
    public class LoginServiceImpl : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public LoginServiceImpl(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<(IdentityResult Result, Guid? ClientId)> Signup(RegisterModel register)
        {
            if (register.Role != "SAV" && register.Role != "CLIENT")
            {
                return (IdentityResult.Failed(new IdentityError { Description = "Role must be 'SAV' or 'CLIENT'." }), null);
            }

            var user = new ApplicationUser
            {
                UserName = register.Email,
                FullName = register.FullName,
                Address = register.Address,
                Email = register.Email,
                PhoneNumber = register.Phone,
                Role = register.Role
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
                return (result, null);

            if (!await _roleManager.RoleExistsAsync(register.Role))
            {
                return (IdentityResult.Failed(new IdentityError { Description = $"Role '{register.Role}' does not exist." }), null);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, register.Role);

            Guid? clientId = null;

            if (register.Role == "CLIENT")
            {
                var client = new Client
                {
                    Id = Guid.NewGuid(),
                    Name = user.FullName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    UserId = user.Id,
                    User = user
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                clientId = client.Id;
            }

            return (roleResult, clientId);
        }

        public async Task<(SignInResult Result, ApplicationUser User, string[] Roles, Guid? ClientId)> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (SignInResult.Failed, null, null, null);

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            string[] roles = null;
            Guid? clientId = null;

            if (signInResult.Succeeded)
            {
                roles = (await _userManager.GetRolesAsync(user)).ToArray();

                if (roles.Contains("CLIENT"))
                {
                    var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    clientId = client?.Id;
                }
            }

            return (signInResult, user, roles, clientId);
        }
    }

}
