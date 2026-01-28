using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.IdentityDTOs
{
    public record RegisterDTO(string Name, string DisplayName, string UserName,[EmailAddress] string Email, string Password ,[Phone] string phone);
    
    
}
