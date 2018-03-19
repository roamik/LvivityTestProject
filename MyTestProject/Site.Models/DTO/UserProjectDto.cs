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
    public virtual string UserId { get; set; }

    public virtual UserDto User { get; set; }

    public virtual string ProjectId { get; set; }

    public virtual ProjectDto Project { get; set; }

    public LinkStatus Status { get; set; }
  }
}
