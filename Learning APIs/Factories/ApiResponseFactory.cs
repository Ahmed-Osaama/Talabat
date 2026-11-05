using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Learning_APIs.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value?.Errors.Any() == true)
                .Select(error => new ValidationError
                {
                    Field = error.Key,
                    Messages = error.Value?.Errors.Select(e => e.ErrorMessage) ?? new List<string>()
                });
            var response = new ValidationErrorResponse
            {
                statusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "One or more Validation Error happened",
                Errors = errors
                
            };
            return new BadRequestObjectResult(response);
        }
    }
}
