using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Configurations;

public class ConnectedPersonConfiguration : IEntityTypeConfiguration<ConnectedPerson>
{
    public void Configure(EntityTypeBuilder<ConnectedPerson> builder)
    {
        builder.HasKey(x => new { x.Id });
        
        builder
            .Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(x => x.ConnectionType)
            .IsRequired();

        builder.HasOne(x => x.ConnectPerson)
            .WithMany()
            .HasForeignKey(x => x.ConnectPersonId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Person)
            .WithMany(x => x.ConnectedPersons)
            .HasForeignKey(x => x.PersonId);
        
        builder
            .Property(x => x.ConnectionType)
            .HasConversion<int>();
    }
}