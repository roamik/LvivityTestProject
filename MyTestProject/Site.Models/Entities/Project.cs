using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Site.Models.Entities
{
  public class Project : Entity<Guid>
  {
    public Guid OwnerId { get; set; } // creator id

    public string Name { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string ImagePath { get; set; }


    //navigation property

    public List<UserProject> LinkedUsers { get; set; }// users linked to this project

    
  }
}
