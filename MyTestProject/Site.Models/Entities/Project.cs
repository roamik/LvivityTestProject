using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Models.Entities
{
  public class Project : Entity
  {
    public Project()
    {
      Id = Guid.NewGuid().ToString();
    }
    [Key]
    public override string Id { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; } // creator id

    public virtual User User { get; set; } // creator navigation

    public string Name { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    //navigation property

    public virtual List<UserProject> LinkedUsers { get; set; } = new List<UserProject>();// users linked to this project

    
  }
}
