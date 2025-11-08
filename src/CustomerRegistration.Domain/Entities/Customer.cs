using CustomerRegistration.Domain.Common;
using CustomerRegistration.Domain.Enum;

namespace CustomerRegistration.Domain.Entities;

public class Customer : Entity<Customer>
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string PasswordHash { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public DateTime? LastLoginAt { get; protected set; }
    public CustomerStatus Status { get; protected set; }

    protected Customer() { }

    public Customer(string name, string email, string passwordHash, string phoneNumber, CustomerStatus status = CustomerStatus.HasChangePassword)
    {
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
}
