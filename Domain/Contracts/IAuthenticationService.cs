using Shared.DTOs.IdentityModule;
using Shared.DTOs.OrderModule;
namespace Domain.Contracts
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        //Get Current user
        Task<UserResultDto> GetCurrentUserAsync(string email);
        //Check if email exists
        Task<bool> CheckEmailExist(string email);
        //Get addresss
        Task<AddressDto> GetAddressAsync(string email);
        //update address
        Task<AddressDto> UpdateAddressAsync(string email, AddressDto addressDto);
    }
}
