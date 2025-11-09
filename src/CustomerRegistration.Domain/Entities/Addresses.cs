using CustomerRegistration.Domain.Common;

namespace CustomerRegistration.Domain.Entities;

public class Addresses : Entity<Addresses>
{
    public virtual Customer Customer { get; protected set; }
    public int CustomerId { get; protected set; }
    public string Street { get; protected set; }
    public string City { get; protected set; }
    public string State { get; protected set; }
    public string Country { get; protected set; }
    public string ZipCode { get; protected set; }

    protected Addresses() { }

    public Addresses(Customer customer, string street, string city, string state, string country, string zipCode)
    {
        Customer = customer;
        CustomerId = customer.Id;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }
}
