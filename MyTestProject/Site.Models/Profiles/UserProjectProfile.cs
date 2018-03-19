using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Site.Models.DTO;
using Site.Models.Entities;

namespace Site.Models.Profiles
{
    class UserProjectProfile : Profile
    {
      public UserProjectProfile()
      {
        CreateMap<UserProject, UserProjectDto>().MaxDepth(1);
      }
  }
}
