using CustomerRegistration.Domain.Common;
using CustomerRegistration.Domain.Enum;

namespace CustomerRegistration.Domain.Entities;

public class Customer : Entity<Customer>
{
    private readonly List<Addresses> _addresses = [];

    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string PasswordHash { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public DateTime? LastLoginAt { get; protected set; }
    public CustomerStatus Status { get; protected set; }
    public IReadOnlyCollection<Addresses> Addresses => _addresses.AsReadOnly();

    protected Customer() { }

    public Customer(string name, string email, string passwordHash, string phoneNumber, CustomerStatus status = CustomerStatus.HasChangePassword)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));
            
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));

        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        Status = status;
    }

    public void UpdateLastLogin(DateTime loginTime)
    {
        LastLoginAt = loginTime;
    }

    public void UpdateStatus(CustomerStatus status)
    {
        if (Status == CustomerStatus.Deleted)
            throw new InvalidOperationException("Cannot change status of a deleted customer.");

        Status = status;
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Name cannot be empty.", nameof(newName));

        Name = newName;
    }

    public void ChangeEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email cannot be empty.", nameof(newEmail));

        Email = newEmail;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
    }

    public void ChangePhoneNumber(string newPhoneNumber)
    {
        if (string.IsNullOrWhiteSpace(newPhoneNumber))
            throw new ArgumentException("Phone number cannot be empty.", nameof(newPhoneNumber));

        PhoneNumber = newPhoneNumber;
    }

    public void Activate()
    {
        UpdateStatus(CustomerStatus.Active);
    }

    public void Deactivate()
    {
        UpdateStatus(CustomerStatus.Inactive);
    }

    public void Suspended()
    {
        UpdateStatus(CustomerStatus.Suspended);
    }

    public void AddAddress(Addresses address)
    {
        if (address == null)
            throw new ArgumentNullException(nameof(address), "Address cannot be null.");

        if (_addresses.Contains(address))
            throw new InvalidOperationException("Address already exists for this customer.");

        _addresses.Add(address);
    }
    
    public void RemoveAddress(Addresses address)
    {
        if (address == null)
            throw new ArgumentNullException(nameof(address), "Address cannot be null.");

        if (!_addresses.Contains(address))
            throw new InvalidOperationException("Address does not exist for this customer.");

        _addresses.Remove(address);
    }
}
