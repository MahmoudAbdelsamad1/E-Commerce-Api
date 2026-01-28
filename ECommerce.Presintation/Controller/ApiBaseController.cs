using ECommerce.Shared.CommonResults;
using ECommerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presintation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        // handle result without value 
        protected IActionResult HandleResult(Result result)
        {

            if (result.IsSuccess) return NoContent();
            else
            return HandleError(result.Error);

        }

        protected ActionResult<ProductDTO> HandleResult<ProductDTO>(Result<ProductDTO> result)
        {

            if (result.IsSuccess) return Ok(result.Value);
            else
                return   HandleError(result.Error);

        }

        private ActionResult HandleError(IReadOnlyList<Error> errors)
        {
            // No errors provided 
            if(!errors.Any()) return Problem(statusCode:StatusCodes.Status500InternalServerError, title: " Unexpected Error Occurred");
           
            // Multipale Errors Provided 
            if(errors.All(e => e.ErrorType == ErrorType.Validation)) return HandleMultipaleValidationsErrors(errors);

            // Single Errors provided 
           return HandleSingleError(errors[0]);

        }


        private ActionResult HandleSingleError(Error error)
        {

            return Problem(title: error.Code,
                detail: error.Description,
                type: error.ErrorType.ToString(),
                statusCode: MappingStatusCodeFromError(error.ErrorType));

        }


        private int MappingStatusCodeFromError(ErrorType error)
        {

            switch (error)
            {
                case ErrorType.NotFound: return StatusCodes.Status404NotFound;

                case ErrorType.Forbidden: return StatusCodes.Status403Forbidden;

                case ErrorType.Validation: return StatusCodes.Status400BadRequest;

                case ErrorType.Unauthorized: return StatusCodes.Status401Unauthorized;

                case ErrorType.Failure: return StatusCodes.Status404NotFound;

                case ErrorType.InvalidCredentials: return StatusCodes.Status401Unauthorized;

                default: return StatusCodes.Status500InternalServerError;

            }
        }

        private ActionResult HandleMultipaleValidationsErrors(IReadOnlyList<Error> errors) {

            var modelState = new ModelStateDictionary();

            foreach (var error in errors) {
            
                modelState.AddModelError(error.Code, error.Description);
            
            
            }

            return ValidationProblem(modelState);
        
        }
    }
}
