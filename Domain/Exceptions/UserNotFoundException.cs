namespace Domain.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string email) : base($"User with email : {email} was not found.")
        {
        }
    }
}
