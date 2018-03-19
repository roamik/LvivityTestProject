using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Models.Entities
{
  public class Template : Entity
  {
    public Template()
    {
      Id = Guid.NewGuid().ToString();
    }

    [Key]
    public override string Id { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    //navigation property
    public virtual User User { get; set; }
  }
}