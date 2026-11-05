using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Common;
using Shared.DTOs.IdentityModule;
using Shared.DTOs.OrderModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations
{
    public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> _options , IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailExist(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<AddressDto> GetAddressAsync(string email)
        {
            var User =await _userManager.Users.Include(user => user.Address).FirstOrDefaultAsync(u => u.Email == email) ??
                throw new UserNotFoundException(email);
            return _mapper.Map<AddressDto>(User.Address);
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email) ??
                throw new UserNotFoundException(email);
            return new UserResultDto(user.DisplayName, GenerateTokenAsync(user).Result, user.Email);
        }
        public async Task<AddressDto> UpdateAddressAsync(string email, AddressDto addressDto)
        {
            var User = await _userManager.Users.Include(user => user.Address).FirstOrDefaultAsync(u => u.Email == email) ??
                throw new UserNotFoundException(email);
            if(User.Address != null)
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street = addressDto.Street;
            }
            else
            {
                var address = _mapper.Map<Address>(addressDto);
                User.Address = address;
            }
            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDto>(User.Address);
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user =await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)  throw new UnauthorizedException();
            var isPasswordValid =await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid) throw new UnauthorizedException();
            return new UserResultDto(user.DisplayName, await GenerateTokenAsync(user), user.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Username
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                // Fix: throw ValidationErros instead of returning it
                throw new ValidationErros(errors);
            }
            return new UserResultDto(user.DisplayName, await GenerateTokenAsync(user), user.Email);
        }

        private async Task<string> GenerateTokenAsync(User user)
        {
            var jwtOptions = _options.Value;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            foreach(var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: jwtOptions.Issuer, audience: jwtOptions.Audience, claims: claims, expires: DateTime.Now.AddDays(jwtOptions.ExpireInDays), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
