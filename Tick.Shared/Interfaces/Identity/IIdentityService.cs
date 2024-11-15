using Tick.Shared.DTO.Identity;

namespace Tick.Shared.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<string> RegisterUserAsync(UserRegisterDto userRegisterDto);
        Task<bool> LoginAsync(UserLoginDto userLoginDto);
        Task<string> WhoAmI();
        Task LogoutAsync();
    }
}
