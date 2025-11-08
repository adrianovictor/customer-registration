using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerRegistration.Infrastructure.Mappings.Entities;

public class CustomerMap : EntityMap<Customer>
{
    protected override void Map(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(_ => _.Name).IsRequired().HasMaxLength(120);
        builder.Property(_ => _.Email).IsRequired().HasMaxLength(150);
        builder.Property(_ => _.PasswordHash).IsRequired().HasMaxLength(256);
        builder.Property(_ => _.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.Property(_ => _.LastLoginAt).HasColumnType("datetime2");
        builder.Property(_ => _.Status).IsRequired();
    }
}
