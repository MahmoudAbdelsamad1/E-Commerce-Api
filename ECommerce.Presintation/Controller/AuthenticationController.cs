using ECommerce.Service.Abstraction.IAuthenticationServices;
using ECommerce.Shared.CommonResults;
using ECommerce.Shared.IdentityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presintation.Controller
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;

        }
                
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var Result = await _authenticationServices.LoginAsync(loginDTO);

            return HandleResult(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var Result = await _authenticationServices.RegisterAsync(registerDTO);

            return HandleResult(Result);
        }
            
        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)

        {
          var Result = await _authenticationServices.CheckEmailAsync(email: Email);
            return Ok(value: Result);

        }

        [Authorize]
        [HttpGet( "CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()

        {
                
            var Email = User.FindFirstValue( ClaimTypes.Email)!;
            var Result = await _authenticationServices.GetUserByEmailAsync(email: Email);
            return HandleResult(result: Result);

        }
    }
}
