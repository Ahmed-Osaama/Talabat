namespace Domain.Exceptions
{
    public class ValidationErros : Exception
    {
        public IEnumerable<string> Errors { get; set; } = [];
        public ValidationErros(IEnumerable<string> errors) : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }
}
