namespace Domain.Entities.OrderModule
{
    public class Address
    {
        public Address(int id, string firstName, string lastName, string city, string country, string street)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Country = country;
            Street = street;
        }
        public Address()
        {
            
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
    }
}
