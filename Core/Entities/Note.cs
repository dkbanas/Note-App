using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    public string UserEmail { get; set; }

    public Note()
    {
    }

    public Note(string? title, string? description, string userEmail)
    {
        Title = title;
        Description = description;
        UserEmail = userEmail;
    }
}