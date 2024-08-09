using System.ComponentModel.DataAnnotations;

namespace API.DTO;

public class UpdatePasswordDto
{
    [Required]
    public string CurrentPassword { get; set; }
    [Required]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Minimal password requirements:6 characters,1 uppercase, 1 lowercase,1 number,1 special character")]
    public string NewPassword { get; set; }
}