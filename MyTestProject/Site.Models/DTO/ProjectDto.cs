using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.Models.DTO
{
  public class ProjectDto
  {
    public virtual Guid? Id { get; set; }

    public string UserId { get; set; }

    public virtual UserDto User { get; set; }

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

    public virtual List<UserProjectDto> LinkedUsers { get; set; }  = new List<UserProjectDto>();
  }
}
