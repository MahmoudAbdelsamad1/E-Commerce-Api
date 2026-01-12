using ECommerce.Service.Abstraction.IAuthenticationServices;
using ECommerce.Shared.CommonResults;
using ECommerce.Shared.IdentityDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
