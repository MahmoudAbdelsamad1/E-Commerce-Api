using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.Service.Abstraction.IAuthenticationServices;
using ECommerce.Shared.CommonResults;
using ECommerce.Shared.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.AuthenticationServices
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration configuration;

        public AuthenticationServices(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this._userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (User is null)
                return Result<UserDTO>.Failed(Error.InvalidCredentials("User.InvalidCredentials"));
            var IsPassworedValid = await _userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (!IsPassworedValid) return Result<UserDTO>.Failed(Error.InvalidCredentials("User.InvalidCredentials"));

            var token = await CreateTokenAsync(User);
            UserDTO user = new UserDTO(loginDTO.Email, User.DisplayName, token);

            return Result<UserDTO>.Ok(user);
        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser()
            {

                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.phone
            };

            var IdentityResult = await _userManager.CreateAsync(User, registerDTO.Password);
            if (IdentityResult.Succeeded)
            {
                var token = await CreateTokenAsync(User);

                return Result<UserDTO>.Ok(new UserDTO(User.Email, User.DisplayName, token));
            }
            return Result<UserDTO>.Failed(IdentityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList());
        }

        public async Task<bool> CheckEmailAsync(string email)

        {

            var User = await _userManager.FindByEmailAsync(email);

            return User != null;

        }
        //

        public async Task<Result<UserDTO>> GetUserByEmailAsync(string email) { 

       
        var User = await _userManager.FindByEmailAsync(email);
         
       if (User is null)
       return Result<UserDTO>.Failed(new Error("not.Found", $"No user with email {User.Email} was foudn ",ErrorType.Forbidden));

            return Result<UserDTO>.Ok(new UserDTO(User.Email, User.DisplayName, Token: await CreateTokenAsync(user: User)));

}


        //

        private async Task<string> CreateTokenAsync(ApplicationUser user)

        {
      // Token [Issuer Audience Claims Expires, SigningCredentials]
            var Claims = new List<Claim>(){
            new Claim(type: JwtRegisteredClaimNames.Email,  user. Email!),
            new Claim(type: JwtRegisteredClaimNames.Name, user. Email!),
             };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {

                Claims.Add(item: new Claim(type: ClaimTypes.Role, value: role));

            }

            var Secretkey = configuration["JWTOptions:Secretkey"];
            var Key = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: Secretkey));
            var Cred = new SigningCredentials(key: Key, algorithm: SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
             issuer: configuration["JWTOptions:Issuer"],
             audience: configuration["JWTOptions:Audience"],
             claims: Claims,
             expires: DateTime.UtcNow.AddHours(1),
             signingCredentials: Cred);


            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

    }
}
