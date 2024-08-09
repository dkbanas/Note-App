using System.ComponentModel.DataAnnotations;

namespace API.DTO;

public class UpdateUserDto
{
    [Required]
    public string Username { get; set; }
}