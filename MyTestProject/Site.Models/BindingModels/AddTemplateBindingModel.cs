using System.ComponentModel.DataAnnotations;

namespace Site.Models.BindingModels
{
  public class AddTemplateBindingModel
  {
    [Required]
    [MinLength(4)]
    [MaxLength(200)]
    public string Content { get; set; }
    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    public string Description { get; set; }
    [Required]
    [MinLength(4)]
    [MaxLength(8)]
    public string Name { get; set; }
  }
}
