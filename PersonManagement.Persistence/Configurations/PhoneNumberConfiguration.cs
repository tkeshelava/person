using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Configurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(x => x.PhoneNumberType)
            .HasConversion<int>();
            
        builder.HasOne(x => x.Person)
            .WithMany(p => p.PhoneNumbers)
            .HasForeignKey(x => x.PersonId);
    }
}