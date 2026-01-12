using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.Service.Abstraction.IAuthenticationServices;
using ECommerce.Shared.CommonResults;
using ECommerce.Shared.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.AuthenticationServices
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationServices(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (User is null)
                return Result<UserDTO>.Failed(Error.InvalidCredentials("User.InvalidCredentials"));
            var IsPassworedValid = await _userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (!IsPassworedValid) return Result<UserDTO>.Failed(Error.InvalidCredentials("User.InvalidCredentials"));
            UserDTO user = new UserDTO(loginDTO.Email, User.DisplayName, "token");
            return Result<UserDTO>.Ok(user);
        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser()
            {

                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName= registerDTO.UserName,
                PhoneNumber = registerDTO.phone
            };

            var IdentityResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (IdentityResult.Succeeded)
                return Result<UserDTO>.Ok(new UserDTO(user.Email, user.DisplayName, "token"));

            return Result<UserDTO>.Failed(IdentityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList());
        }
    }
}
