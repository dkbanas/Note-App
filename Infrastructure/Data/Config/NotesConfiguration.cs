using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class NotesConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(30);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
    }
}