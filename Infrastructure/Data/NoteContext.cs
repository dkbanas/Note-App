using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

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
    }
}