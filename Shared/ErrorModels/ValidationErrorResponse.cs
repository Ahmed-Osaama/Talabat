namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int statusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public IEnumerable<ValidationError> Errors { get; set; } = [];
    }
}
