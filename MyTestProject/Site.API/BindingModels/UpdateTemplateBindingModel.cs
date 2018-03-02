using System;
using System.ComponentModel.DataAnnotations;

namespace Site.API.BindingModels
{
  public class UpdateTemplateBindingModel
  {
    public virtual Guid? Id { get; set; }

    public string Content { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }
  }
}
