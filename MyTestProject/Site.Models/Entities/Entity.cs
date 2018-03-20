using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Site.Models.Entities
{
  public class Entity<T>
  {
    [Key]
    public T Id { get; set; }
  }
}
