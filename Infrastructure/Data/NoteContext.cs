using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data;

public class  NoteContext : DbContext
{
    protected NoteContext()
    {
    }

    //Allows for configuration of the database (Connection String)
    public NoteContext(DbContextOptions<NoteContext> options) : base(options)
    {
    }
    
    //Tables in DB
    public DbSet<Note> Notes { get; set; }

    //Configuring entities mapped to database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating( modelBuilder );
        modelBuilder.ApplyConfigurationsFromAssembly( Assembly.GetExecutingAssembly() );
        modelBuilder.UseSerialColumns();
        if (Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL")
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                var dateTimeProperties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));
                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                }

                foreach (var property in dateTimeProperties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
                }
            }
        }
    }
}