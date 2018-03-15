using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Site.Models.Enums;

namespace Site.Models.Entities
{
  public class UserProject
  {
    [ForeignKey("User")]
    [Required]
    public string UserId { get; set; }

    public virtual User User { get; set; }

    [ForeignKey("Project")]
    [Required]
    public string ProjectId { get; set; }

    public virtual Project Project { get; set; }

    public LinkStatus Status { get; set; }
  }
}
