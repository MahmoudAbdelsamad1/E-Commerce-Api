using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.CustomExceptions
{
    public abstract class NonFoundException(string message) : Exception(message)
    {
    }
    
    public sealed class ProductNonFoundException(string message) : NonFoundException(message) {
    
    
    }

    public sealed class BasketNonFoundException(string message) : NonFoundException(message)
    {


    }
}
