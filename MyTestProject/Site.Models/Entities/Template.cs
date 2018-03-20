using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Models.Entities
{
  public class Template : Entity<Guid>
  {
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

  }
}
