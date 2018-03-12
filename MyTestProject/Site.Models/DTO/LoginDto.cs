using System.ComponentModel.DataAnnotations;

namespace Site.Models.DTO
{
  public class LoginDto
  {

    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email isnt valid")]
    [EmailAddress(ErrorMessage = "Email isnt valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])?(?=.*\d)(?=.*[$@$!%*?&-])?[A-Za-z\d$@$!%*?&-]{8,}", ErrorMessage = "Password not valid!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

  }
}
