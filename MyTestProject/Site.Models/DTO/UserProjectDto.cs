using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Site.Models.Entities;
using Site.Models.Enums;

namespace Site.Models.DTO
{
  public class UserProjectDto
  {
    public virtual Guid UserId { get; set; }

    public virtual UserDto User { get; set; }

    public virtual Guid ProjectId { get; set; }

    public virtual ProjectDto Project { get; set; }

    public LinkStatus Status { get; set; }
  }

  public class UserProjectDtoInput
  {
    public Guid UserId { get; set; }

    public Guid ProjectId { get; set; }
  } 
}
