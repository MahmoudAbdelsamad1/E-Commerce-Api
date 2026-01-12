using ECommerce.Shared.CommonResults;
using ECommerce.Shared.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction.IAuthenticationServices
{
    public interface IAuthenticationServices
    {
        public Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);
        public Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    }
}
