using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Site.Models.DTO;
using Site.Models.Entities;

namespace Site.Models.Profiles
{
  public class TemplateProfile : Profile
  {
    public TemplateProfile()
    {
      CreateMap<Template, TemplateDto>().MaxDepth(1);
      CreateMap<TemplateDto, Template>().MaxDepth(1);
    }
  }
}
