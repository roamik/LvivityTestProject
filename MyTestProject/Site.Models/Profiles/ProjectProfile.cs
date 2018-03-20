using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Site.Models.DTO;
using Site.Models.Entities;

namespace Site.Models.Profiles
{
    class ProjectProfile : Profile
    {
      public ProjectProfile()
      {
        CreateMap<Project, ProjectDto>().MaxDepth(1);
        CreateMap<ProjectDto, Project>().MaxDepth(1);
        CreateMap<ProjectDtoInput, Project>();
        CreateMap<Project, ProjectDtoInput>();

        CreateMap<UserProjectDtoInput, UserProject>();
        CreateMap<UserProject, UserProjectDtoInput>();
      
    }
  }
}
