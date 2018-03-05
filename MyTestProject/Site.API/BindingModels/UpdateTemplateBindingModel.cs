using System;
using System.ComponentModel.DataAnnotations;

namespace Site.API.BindingModels
{
  public class UpdateTemplateBindingModel
  {
    public virtual Guid? Id { get; set; }

    [Required]
    [MinLength(4)]
    public string Content { get; set; }

    [Required]
    [MinLength(4)]
    public string Description { get; set; }

    [Required]
    [MinLength(4)]
    public string Name { get; set; }
  }
}
