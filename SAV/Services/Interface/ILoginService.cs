using Microsoft.AspNetCore.Identity;
using SAV.Models;

namespace SAV.Services.Interface
{
    public interface ILoginService
    {
        /*
        //string Login(LoginModel login);
        Task<IdentityResult> Signup(RegisterModel register);
        Task<(SignInResult Result, ApplicationUser User, string[] Roles)> LoginAsync(LoginModel model);

    }*/
        Task<(IdentityResult Result, Guid? ClientId)> Signup(RegisterModel register);
        Task<(SignInResult Result, ApplicationUser User, string[] Roles, Guid? ClientId)> LoginAsync(LoginModel model);

    }
}