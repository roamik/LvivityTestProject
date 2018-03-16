using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models.DTO
{
  public class ProjectDto
  {
    public virtual Guid? Id { get; set; }

    public string UserId { get; set; }

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
