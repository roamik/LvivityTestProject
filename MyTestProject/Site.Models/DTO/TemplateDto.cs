using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models.DTO
{
  public class TemplateDto
  {
    public virtual Guid? Id { get; set; }

    public Guid UserId { get; set; }

    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    public string Description { get; set; }

    [Required]
    [MinLength(4)]
    [MaxLength(200)]
    public string Content { get; set; }
  }
}
