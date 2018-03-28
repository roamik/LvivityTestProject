using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Site.Models.DTO
{
  public class ProjectDto
  {
    public virtual Guid Id { get; set; }

    public virtual Guid OwnerId { get; set; }

    public virtual UserDto Owner { get; set; }

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

    public string ImagePath { get; set; }

    public virtual List<UserProjectDto> LinkedUsers { get; set; }
  }

  public class ProjectDtoInput
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string ImagePath { get; set; }

    public List<UserProjectDtoInput> LinkedUsers { get; set; }
  }
}
