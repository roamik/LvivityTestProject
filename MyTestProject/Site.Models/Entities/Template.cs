using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Site.Models.Entities.Interfaces;

namespace Site.Models.Entities
{
  public class Template : IEntity
  {
    [ForeignKey("User")]
    public string UserId { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    //navigation property
    public User User { get; set; }
  }
}
