namespace Domain.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string id) : base($"The Basket with id : {id} Couldn't be found")
        {
            
        }
    }
}
