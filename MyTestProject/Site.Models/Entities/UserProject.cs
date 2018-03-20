using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Site.Models.Enums;

namespace Site.Models.Entities
{
  public class UserProject : Entity<Guid>
  {
    public Guid UserId { get; set; }

    public Guid ProjectId { get; set; }

    public LinkStatus Status { get; set; }
  }
}
