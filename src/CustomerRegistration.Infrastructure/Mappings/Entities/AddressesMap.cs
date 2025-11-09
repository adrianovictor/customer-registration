using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Mappings.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerRegistration.Infrastructure.Mappings.Entities;

public class AddressesMap : EntityMap<Addresses>
{
    protected override void Map(EntityTypeBuilder<Addresses> builder)
    {
        builder.Property(_ => _.Street).IsRequired().HasMaxLength(200);
        builder.Property(_ => _.City).IsRequired().HasMaxLength(100);
        builder.Property(_ => _.State).IsRequired().HasMaxLength(100);
        builder.Property(_ => _.Country).IsRequired().HasMaxLength(100);
        builder.Property(_ => _.ZipCode).IsRequired().HasMaxLength(10);
    }
}
