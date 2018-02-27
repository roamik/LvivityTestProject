using MyTestProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.Models
{
  public class Template
  {
    [ForeignKey("User")]
    public string UserId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Decription { get; set; }

    public string Content { get; set; }

    //navigation property
    public User User { get; set; }
  }
}
