using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.BindingModels
{
  public class RegisterBindingModel
  {

    public string Name { get; set; }

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
