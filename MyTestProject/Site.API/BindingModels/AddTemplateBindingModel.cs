using System.ComponentModel.DataAnnotations;

namespace Site.API.BindingModels
{
  public class AddTemplateBindingModel
  {
    [Required]
    public string Content { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Name { get; set; }
  }
}
