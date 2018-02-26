using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.BindingModels
{
  public class LoginBindingModel
  {

    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email isnt valid")]
    [EmailAddress(ErrorMessage = "Email isnt valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

  }
}
