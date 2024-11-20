using Microsoft.AspNetCore.Identity;
using Tick.Identity.Models;
using Tick.Models.DTO.Identity;
using Tick.Shared.Interfaces.Identity;

namespace Tick.Identity.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(UserLoginDto userLoginDto)
        {
            SignInResult result = await this._signInManager.PasswordSignInAsync(userLoginDto.Email,
                userLoginDto.Password, isPersistent: true, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await this._signInManager.SignOutAsync();
        }

        public async Task<string> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            ApplicationUser user = new()
            {
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Email,
                FullName = userRegisterDto.FullName
            };

            IdentityResult result = await this._userManager.CreateAsync(user, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return user.Id;
        }

        public async Task<string> WhoAmI()
        {
            ApplicationUser? user = await this._userManager.GetUserAsync(this._signInManager.Context.User);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            return user.FullName;
        }
    }
}
