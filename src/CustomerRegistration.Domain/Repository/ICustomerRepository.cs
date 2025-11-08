using CustomerRegistration.Domain.Entities;

namespace CustomerRegistration.Domain.Repository;

public interface ICustomerRepository
{
    Task SaveAsync(Customer customer);
    Task DeleteAsync(Customer customer);
}
