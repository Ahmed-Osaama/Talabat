namespace Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = "Invalid Email or Password") : base(message)
        {

        }
    }
}
