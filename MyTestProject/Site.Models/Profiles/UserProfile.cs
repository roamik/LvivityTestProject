using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Site.Models.DTO;
using Site.Models.Entities;

namespace Site.Models.Profiles
{
  class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, UserDto>().MaxDepth(1);
      CreateMap<UserDto, User>().MaxDepth(1);
    }
  }
}
