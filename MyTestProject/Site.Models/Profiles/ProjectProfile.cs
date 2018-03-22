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
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();
        CreateMap<ProjectDtoInput, Project>();
        CreateMap<Project, ProjectDtoInput>();

        CreateMap<UserProjectDtoInput, UserProject>();
        CreateMap<UserProject, UserProjectDtoInput>();
      
    }
  }
}
