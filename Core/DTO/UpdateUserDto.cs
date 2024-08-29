using System.ComponentModel.DataAnnotations;

namespace Core.DTO;
public class UpdateUserDto
{
    [Required]
    public string Username { get; set; }
}