using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .UseIdentityColumn();

        builder
            .Property(x => x.Gender)
            .HasConversion<int>();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.PersonalN)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasColumnType("Date");

        builder.HasMany(x => x.ConnectedPersons)
            .WithOne(x => x.Person)
            .HasForeignKey(x => x.PersonId);
    }
}