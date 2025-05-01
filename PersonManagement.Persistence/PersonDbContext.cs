using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PersonManagement.Domain.Entities;
using PersonManagement.Persistence.Extensions;
using PersonManagement.Persistence.Interceptors;

namespace PersonManagement.Persistence;

public class PersonDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<PhoneNumber> Mobiles { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<ConnectedPerson> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonDbContext).Assembly);

        modelBuilder.EnableSoftDeleted();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(new BaseEntitySaveChangesInterceptor());
}