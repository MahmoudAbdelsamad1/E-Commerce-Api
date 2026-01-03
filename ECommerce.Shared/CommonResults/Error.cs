using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommonResults
{
    public class Error
    {
        public string Code { get; }

        public string Description { get; }

        public ErrorType ErrorType { get; }


        public Error(string code , string discription , ErrorType errro)
        {
            Code = code;
            Description = discription;
            ErrorType = errro;
        }

        //Failure = 0,
        //Validation = 1,
        //NotFound = 2,
        //Unauthorized = 3,
        //Forbidden = 4,
        //InvalidCredentials = 5,

        public static Error Failure(string code = "General.Failure", string discription = "A General Failure has occurred ") { 
        
            return new Error(code, discription, ErrorType.Failure);
        }
        public static Error Validation(string code = "General.Validation", string discription = "A General Validation has occurred ")
        {

            return new Error(code, discription, ErrorType.Failure);
        }
        public static Error NotFound(string code = "General.NotFound", string discription = "A General NotFound has occurred ")
        {

            return new Error(code, discription, ErrorType.Failure);
        }
        public static Error Unauthorized(string code = "General.Unauthorized", string discription = "A General Unauthorized has occurred ")
        {

            return new Error(code, discription, ErrorType.Failure);
        }
        public static Error Forbidden(string code = "General.Forbidden", string discription = "A General Forbidden has occurred ")
        {

            return new Error(code, discription, ErrorType.Failure);
        }
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string discription = "A General InvalidCredentials has occurred ")
        {

            return new Error(code, discription, ErrorType.Failure);
        }

    }
}
