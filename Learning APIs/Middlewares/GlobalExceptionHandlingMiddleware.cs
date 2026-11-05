using Domain.Exceptions;
using Shared.ErrorModels;

namespace Learning_APIs.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next , ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                await HandleNotFoundException(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundException(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = $"The url you typed {context.Request.Path} Doesn't exist"
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message
            };
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ValidationErros validationErros => HandleValidationError(validationErros, response),
                _ => StatusCodes.Status500InternalServerError
            };
            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationError(ValidationErros validationErros, ErrorDetails response)
        {
            response.Errors = validationErros.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
